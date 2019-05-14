using System;
using System.Collections.Generic;
using System.Linq;
using GooglePlacesImport.Interfaces;
using Importer.Enums;
using Importer.Extensions;
using Importer.Importers;
using Importer.Loggers;
using Quartz;
using log4net;
using Microsoft.Extensions.DependencyInjection;
using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Events;
using Sitecore.DependencyInjection;
using Sitecore.SecurityModel;
using Sitecore.Sites;
using Sitecron.SitecronSettings;


namespace GooglePlacesImport.SitecronTasks
{
    public class ImportGooglePlaces : IJob
    {
        private readonly ILog _logger;
        private readonly IImportJobLogger _importJobLogger;

        public ImportGooglePlaces()
        {
            _logger = LogManager.GetLogger("GooglePlacesImport");
            _importJobLogger = new ImportJobLogger(_logger);
        }

        public void Execute(IJobExecutionContext context)
        {
            try
            {
                var jobDataMap = context.JobDetail.JobDataMap;
                var items = jobDataMap.GetString(SitecronConstants.FieldNames.Items);
                var siteRoots = items.ToGuidList("|");
                if (siteRoots.Any())
                {
                    var itemsToPublish = new List<Guid>();
                    var importWasSuccessful = false;
                    try
                    {
                        foreach (var siteRoot in siteRoots)
                        {
                            try
                            {
                                var rootPath = Database.GetDatabase(Importer.Constants.Sitecore.Databases.Master)
                                    ?.GetItem(new ID(siteRoot))?.Paths.FullPath;
                                var sites = SiteManager.GetSites()
                                    .Where(x => string.Equals(x.Properties["rootPath"], rootPath,
                                        StringComparison.OrdinalIgnoreCase))
                                    .ToList();
                                foreach (var site in sites)
                                {
                                    try
                                    {
                                        var siteContext = SiteContext.GetSite(site.Name);
                                        this._logger.Info(
                                            $"Sitecron - Job {nameof(ImportGooglePlaces)} - site in processing RootPath: {siteContext.RootPath}; Language: {siteContext.Language}; Database: {siteContext.Database}");
                                        using (new SecurityDisabler())
                                        using (new SiteContextSwitcher(siteContext))
                                        using (new EventDisabler())
                                        using (new BulkUpdateContext())
                                        using (var innerScope = ServiceLocator.ServiceProvider
                                            .GetRequiredService<IServiceScopeFactory>().CreateScope())
                                        {
                                            var logs = innerScope.ServiceProvider.GetService<IGooglePlacesImporter>()
                                                .Run();
                                            var currentImportWasSuccessful =
                                                logs.Entries != null &&
                                                logs.Entries.Any(x => x.Action != ImportAction.Undefined);

                                            itemsToPublish.Add(Importer.Constants.ItemGuids.ItemsForGooglePlaces);

                                            _importJobLogger.LogImportResults(logs.Entries);
                                            importWasSuccessful = importWasSuccessful || currentImportWasSuccessful;
                                        }

                                        this._logger.Info(
                                            $"Sitecron - Job {nameof(ImportGooglePlaces)} - import finished successfully");
                                    }
                                    catch (Exception e)
                                    {
                                        this._logger.Error(
                                            $"Sitecron - Job {nameof(ImportGooglePlaces)} - error during processing",
                                            e);
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                this._logger.Error(
                                    $"Sitecron - Job {nameof(ImportGooglePlaces)} - error during site processing", e);
                            }
                        }

                        if (importWasSuccessful)
                        {
                            this.RebuildIndexes();
                            this.PublishItems(itemsToPublish);
                        }
                    }
                    catch (Exception e)
                    {
                        this._logger.Error($"Sitecron - Job {nameof(ImportGooglePlaces)} - error", e);
                    }
                }
                else
                {
                    this._logger.Info(
                        $"Sitecron - Job {nameof(ImportGooglePlaces)} - skipped because no one site is selected");
                }
            }
            catch (Exception e)
            {
                this._logger.Error($"Sitecron - Job {nameof(ImportGooglePlaces)} - error", e);
            }
            this._logger.Info($"Sitecron - Job {nameof(ImportGooglePlaces)} has finished successfully");
        }

        private void PublishItems(List<Guid> itemsToPublish)
        {
            try
            {
                using (new DatabaseSwitcher(Database.GetDatabase(Importer.Constants.Sitecore.Databases.Master)))
                using (var innerScope = ServiceLocator.ServiceProvider.GetRequiredService<IServiceScopeFactory>()
                    .CreateScope())
                {
                    var importer = innerScope.ServiceProvider.GetService<IImporter>();

                    if (importer == null)
                    {
                        this._logger.Warn(
                            $"Sitecron - Job {nameof(ImportGooglePlaces)} - nullable importer - items haven't been published");
                        return;
                    }

                    if (itemsToPublish == null || !itemsToPublish.Any())
                    {
                        this._logger.Warn(
                            $"Sitecron - Job {nameof(ImportGooglePlaces)} - empty roots - items haven't been published");
                        return;
                    }

                    foreach (var root in itemsToPublish.Where(x => x != Guid.Empty))
                    {
                        var rootLogs = importer.Publish(root);
                        this._importJobLogger.LogImportResults(rootLogs);
                    }
                }
            }

            catch (Exception exception)
            {
                this._logger.Error($"Sitecron - Job {nameof(ImportGooglePlaces)} - publishing error", exception);
            }
        }

        private void RebuildIndexes()
        {
            throw new NotImplementedException();
        }
    }
}