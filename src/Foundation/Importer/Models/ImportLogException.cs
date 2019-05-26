using System;

namespace Importer.Models
{
    public class ImportLogException : Exception
    {
        public ImportLogEntry Entry { get; set; }
    }
}