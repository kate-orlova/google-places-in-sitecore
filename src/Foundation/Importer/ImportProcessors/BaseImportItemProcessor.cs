using Glass.Mapper.Sc;
using Importer.Models;
using Importer.Search;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using Sitecore.Globalization;
using System.Collections.Generic;
using Importer.Repositories;
using Sitecore.Data.Managers;

namespace Importer.ImportProcessors
{
    public abstract class BaseImportItemProcessor<TItem, TImportObj> : IBaseImportItemProcessor<TItem, TImportObj> where TItem : GlassBase, IGlassBase
    {
        protected ISitecoreContext Context { get; }
        protected readonly string LocationPathOverride;
        protected virtual bool CacheItems => true;
        protected IDictionary<string, IList<TItem>> ItemsCache;
        protected IGenericSitecoreItemRepository<TItem> GenericItemRepository { get; }

        protected BaseImportItemProcessor(ISitecoreContext sitecoreContext, string locationPathOverride = null)
        {
            this.Context = sitecoreContext;
            this.LocationPathOverride = locationPathOverride;
            this.ItemsCache = new Dictionary<string, IList<TItem>>();
            var searchManager = ServiceLocator.ServiceProvider.GetService<IBaseSearchManager<TItem>>();
            this.GenericItemRepository = new GenericSitecoreItemRepository<TItem>(sitecoreContext, searchManager);
        }

        public TItem ProcessItem(TImportObj importObj, IEnumerable<Language> languageVersions, string pathOverride = null)
        {
            var defaultLanguage = LanguageManager.DefaultLanguage;
            return null;
        }
    }
}
