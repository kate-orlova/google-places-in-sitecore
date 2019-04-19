using Importer.Models;
using System;
using System.Collections.Generic;
using Sitecore.ContentSearch;
using Sitecore.Globalization;

namespace Importer.Search
{
    public interface IBaseSearchManager<out TItem> where TItem : class, IGlassBase
    {
        TItem FindItem(Guid itemGuid);
        IEnumerable<TItem> GetAllWithTemplate(IProviderSearchContext context = null, bool mapResults = false, bool inferType = false, bool isLazy = false, string path = null, Language language = null);
    }
}
