using Importer.Loggers;
using Quartz;
using log4net;


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
            throw new System.NotImplementedException();
        }
    }
}