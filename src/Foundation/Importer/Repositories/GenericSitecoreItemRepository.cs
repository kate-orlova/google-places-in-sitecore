using Importer.Models;
using Sitecore.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using Glass.Mapper.Sc;
using Importer.Search;
using Sitecore.Data;

namespace Importer.Repositories
{
    public class GenericSitecoreItemRepository<TItem> : IGenericSitecoreItemRepository<TItem> where TItem : GlassBase, IGlassBase
    {
        private readonly ISitecoreContext context;
        private readonly IBaseSearchManager<TItem> searchManager;

        public GenericSitecoreItemRepository(ISitecoreContext sitecoreContext, IBaseSearchManager<TItem> searchManager)
        {
            this.context = sitecoreContext;
            this.searchManager = searchManager;
        }

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
            return language == null
                ? this.context.Query<TItem>(query, inferType: true)
                : this.context.Query<TItem>(query, language, inferType: true);
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
            var id = new ID(item.Id);
            this.context.Database.Caches.DataCache.RemoveItemInformation(id);
            this.context.Database.Caches.ItemCache.RemoveItem(id);
            this.context.Database.Caches.PathCache.RemoveKeysContaining(id.ToString());
        }
    }
}
