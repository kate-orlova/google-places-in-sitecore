using System;
using Importer.Enums;

namespace Importer.Models
{
    class ImportLogEntry
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public MessageLevel Level { get; set; }
    }
}