using System;
using System.Collections.Generic;
using Importer.Models;
using Sitecore.Globalization;

namespace Importer.Repositories
{
    public interface IGenericSitecoreItemRepository<TItem> where TItem : GlassBase, IGlassBase
    {
        TItem GetByGuid(Guid id);

        IEnumerable<TItem> GetByPath(string path, Language language = null, bool mapDatabaseFields = false);

        TItem GetOneByQuery(string query);

        IEnumerable<TItem> GetByQuery(string query, Language language = null);

        TItem Create(string itemName, GlassBase parent, Language language = null);

        TItem Create(string itemName, string path, Language language = null);

        void Save(TItem item);
    }
}