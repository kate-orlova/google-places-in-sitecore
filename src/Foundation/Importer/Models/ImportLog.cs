﻿using System.Collections.Generic;

namespace Importer.Models
{
    public class ImportLog
    {
        public List<ImportLogEntry> Entries { get; set; }
        public int ImportedItems { get; set; }

        public ImportLog()
        {
            Entries = new List<ImportLogEntry>();
            ImportedItems = 0;
        }
    }
}