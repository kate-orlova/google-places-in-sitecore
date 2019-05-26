using Importer.Enums;
using Importer.Models;
using log4net;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            var rejected = importResults.Where(x => x.Action == ImportAction.Rejected).ToList();
            var deleted = importResults.Where(x => x.Action == ImportAction.Deleted).ToList();

            foreach (var generalMessage in generalMessages)
            {
                logger.Info(generalMessage.Message);
            }

            var stats = new StringBuilder();
            if (imported.Any() || rejected.Any() || deleted.Any())
            {
                stats.AppendLine(logger.Logger.Name);
                stats.AppendLine($"{imported.Count} imported");
                stats.AppendLine($"{rejected.Count} rejected");
                stats.AppendLine($"{deleted.Count} deleted");
                stats.AppendLine();

                stats.AppendLine("Imported items");
                foreach (var importedItem in imported)
                {
                    stats.AppendLine(importedItem.Message);
                }

                stats.AppendLine();
                stats.AppendLine("Rejected items");
                foreach (var rejectedItem in rejected)
                {
                    stats.AppendLine(rejectedItem.Message);
                }

                stats.AppendLine();
                stats.AppendLine("Deleted items");
                foreach (var deletedItem in deleted)
                {
                    stats.AppendLine(deletedItem.Message);
                }
            }

            logger.Info(stats.ToString());
        }
    }
}