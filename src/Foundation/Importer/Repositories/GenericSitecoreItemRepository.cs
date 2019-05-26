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
    public class GenericSitecoreItemRepository<TItem> : IGenericSitecoreItemRepository<TItem>
        where TItem : GlassBase, IGlassBase
    {
        private readonly ISitecoreContext context;
        private readonly IBaseSearchManager<TItem> searchManager;

        public GenericSitecoreItemRepository(ISitecoreContext sitecoreContext, IBaseSearchManager<TItem> searchManager)
        {
            context = sitecoreContext;
            this.searchManager = searchManager;
        }

        public TItem GetByGuid(Guid id)
        {
            return context.GetItem<TItem>(id, inferType: true);
        }

        public IEnumerable<TItem> GetByPath(string path, Language language = null, bool mapDatabaseFields = false)
        {
            return searchManager.GetAllWithTemplate(path: path, mapResults: mapDatabaseFields, language: language)
                .ToList();
        }

        public TItem GetOneByQuery(string query)
        {
            return context.QuerySingle<TItem>(query, inferType: true);
        }

        public IEnumerable<TItem> GetByQuery(string query, Language language = null)
        {
            return language == null
                ? context.Query<TItem>(query, inferType: true)
                : context.Query<TItem>(query, language, inferType: true);
        }

        public TItem Create(string itemName, GlassBase parent, Language language = null)
        {
            return context.Create<TItem, GlassBase>(parent, itemName, language);
        }

        public TItem Create(string itemName, string path, Language language = null)
        {
            var parent = language == null
                ? context.GetItem<GlassBase>(path)
                : context.GetItem<GlassBase>(path, language);
            return Create(itemName, parent, language);
        }

        public void Save(TItem item)
        {
            context.Save(item, silent: true);
            var id = new ID(item.Id);
            context.Database.Caches.DataCache.RemoveItemInformation(id);
            context.Database.Caches.ItemCache.RemoveItem(id);
            context.Database.Caches.PathCache.RemoveKeysContaining(id.ToString());
        }
    }
}