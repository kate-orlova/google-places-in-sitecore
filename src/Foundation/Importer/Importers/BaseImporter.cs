using Importer.Models;
using Sitecore.Publishing;
using System;
using System.Collections.Generic;
using Glass.Mapper.Sc;
using Sitecore.Data;

namespace Importer.Importers
{
    public class BaseImporter : IImporter
    {
        protected readonly ISitecoreContext SitecoreContext;

        public BaseImporter(ISitecoreContext sitecoreContext)
        {
            this.SitecoreContext = sitecoreContext;
        }

        public IList<ImportLogEntry> Publish(Guid root, PublishMode mode = PublishMode.Smart,
            bool publishRelatedItems = true,
            bool republishAll = false)
        {
            var logs = new List<ImportLogEntry>();
            var rootItem = this.SitecoreContext.Database?.GetItem(new ID(root));

            throw new NotImplementedException();
        }
    }
}