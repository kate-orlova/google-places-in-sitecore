using System.Collections.Generic;
using Importer.Models;

namespace Importer.Loggers
{
    public interface IImportJobLogger
    {
        void LogImportResults(IList<ImportLogEntry> importResults);
    }
}
