using Importer.Models;
using Sitecore.Publishing;
using System;
using System.Collections.Generic;

namespace Importer.Importers
{
    public class BaseImporter: IImporter
    {
        public IList<ImportLogEntry> Publish(Guid root, PublishMode mode = PublishMode.Smart, bool publishRelatedItems = true,
            bool republishAll = false)
        {
            throw new NotImplementedException();
        }
    }
}
