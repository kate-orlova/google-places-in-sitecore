using Importer.Models;
using Sitecore.Publishing;
using System;
using System.Collections.Generic;
using Glass.Mapper.Sc;

namespace Importer.Importers
{
    public class BaseImporter: IImporter
    {
        protected readonly ISitecoreContext SitecoreContext;

        public IList<ImportLogEntry> Publish(Guid root, PublishMode mode = PublishMode.Smart, bool publishRelatedItems = true,
            bool republishAll = false)
        {
            throw new NotImplementedException();
        }
    }
}
