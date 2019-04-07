using Importer.Models;
using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
        }
    }
}
