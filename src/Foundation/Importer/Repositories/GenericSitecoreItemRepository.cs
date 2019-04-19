using Importer.Models;
using Sitecore.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using Glass.Mapper.Sc;
using Importer.Search;

namespace Importer.Repositories
{
    public class GenericSitecoreItemRepository<TItem> : IGenericSitecoreItemRepository<TItem> where TItem : GlassBase, IGlassBase
    {
        private readonly ISitecoreContext context;
        private readonly IBaseSearchManager<TItem> searchManager;
        public TItem GetByGuid(Guid id)
        {
            return this.context.GetItem<TItem>(id, inferType: true);
        }

        public IEnumerable<TItem> GetByPath(string path, Language language = null, bool mapDatabaseFields = false)
        {
            return searchManager.GetAllWithTemplate(path: path, mapResults: mapDatabaseFields, language: language).ToList();
        }

        public TItem GetOneByQuery(string query)
        {
            return this.context.QuerySingle<TItem>(query, inferType: true);
        }

        public IEnumerable<TItem> GetByQuery(string query, Language language = null)
        {
            throw new NotImplementedException();
        }

        public TItem Create(string itemName, GlassBase parent, Language language = null)
        {
            return this.context.Create<TItem, GlassBase>(parent, itemName, language);
        }

        public TItem Create(string itemName, string path, Language language = null)
        {
            var parent = language == null
                ? this.context.GetItem<GlassBase>(path)
                : this.context.GetItem<GlassBase>(path, language);
            return this.Create(itemName, parent, language);
        }

        public void Save(TItem item)
        {
            this.context.Save(item, silent: true);
        }
    }
}
