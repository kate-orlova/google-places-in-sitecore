using Quartz;
using log4net;


namespace GooglePlacesImport.SitecronTasks
{
    public class ImportGooglePlaces : IJob
    {
        private readonly ILog _logger;
        public void Execute(IJobExecutionContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}