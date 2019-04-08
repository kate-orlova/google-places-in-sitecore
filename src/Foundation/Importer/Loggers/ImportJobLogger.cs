using Importer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Importer.Enums;
using log4net;

namespace Importer.Loggers
{
    public class ImportJobLogger : IImportJobLogger
    {
        private readonly ILog logger;
        public ImportJobLogger(ILog logger)
        {
            this.logger = logger;
        }

        public void LogImportResults(IList<ImportLogEntry> importResults)
        {
            if (importResults == null)
            {
                return;
            }
            var generalMessages = importResults.Where(x => x.Action == ImportAction.Undefined);
            foreach (var generalMessage in generalMessages)
            {
                this.logger.Info(generalMessage.Message);
            }
        }
    }
}
