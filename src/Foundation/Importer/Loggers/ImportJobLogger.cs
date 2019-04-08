using Importer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var imported = importResults.Where(x => x.Action == ImportAction.Imported).ToList();
            foreach (var generalMessage in generalMessages)
            {
                this.logger.Info(generalMessage.Message);
            }
            var stats = new StringBuilder();
            stats.AppendLine(this.logger.Logger.Name);
            stats.AppendLine($"{imported.Count} imported");
            this.logger.Info(stats.ToString());
        }
    }
}
