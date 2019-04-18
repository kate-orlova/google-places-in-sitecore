using Importer.Models;
using Sitecore.Globalization;
using System;
using System.Collections.Generic;

namespace Importer.Repositories
{
    public class GenericSitecoreItemRepository<TItem> : IGenericSitecoreItemRepository<TItem> where TItem : GlassBase, IGlassBase
    {
        public TItem GetByGuid(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TItem> GetByPath(string path, Language language = null, bool mapDatabaseFields = false)
        {
            throw new NotImplementedException();
        }

        public TItem GetOneByQuery(string query)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TItem> GetByQuery(string query, Language language = null)
        {
            throw new NotImplementedException();
        }

        public TItem Create(string itemName, GlassBase parent, Language language = null)
        {
            throw new NotImplementedException();
        }

        public TItem Create(string itemName, string path, Language language = null)
        {
            throw new NotImplementedException();
        }

        public void Save(TItem item)
        {
            throw new NotImplementedException();
        }
    }
}
