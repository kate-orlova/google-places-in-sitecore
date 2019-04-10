﻿using System;
using System.Collections.Generic;
using System.Linq;
using Importer.Extensions;
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
            var jobDataMap = context.JobDetail.JobDataMap;
            var items = jobDataMap.GetString(SitecronConstants.FieldNames.Items);
            var siteRoots = items.ToGuidList("|");
            if (siteRoots.Any())
            {
                var itemsToPublish = new List<Guid>();
                var importWasSuccessful = false;
                foreach (var siteRoot in siteRoots)
                {
                    var rootPath = Database.GetDatabase(Importer.Constants.Sitecore.Databases.Master)?.GetItem(new ID(siteRoot))?.Paths.FullPath;
                    var sites = SiteManager.GetSites()
                        .Where(x => string.Equals(x.Properties["rootPath"], rootPath, StringComparison.OrdinalIgnoreCase))
                        .ToList();
                    foreach (var site in sites)
                    {
                        var siteContext = SiteContext.GetSite(site.Name);
                        this._logger.Info($"Sitecron - Job {nameof(ImportGooglePlaces)} - site in processing RootPath: {siteContext.RootPath}; Language: {siteContext.Language}; Database: {siteContext.Database}");
                        using (new SecurityDisabler())
                        using (new SiteContextSwitcher(siteContext))
                        using (new EventDisabler())
                        using (new BulkUpdateContext())
                        using (var innerScope = ServiceLocator.ServiceProvider
                            .GetRequiredService<IServiceScopeFactory>().CreateScope())
                        {
                        }
                    }
                }
            }
        }
    }
}