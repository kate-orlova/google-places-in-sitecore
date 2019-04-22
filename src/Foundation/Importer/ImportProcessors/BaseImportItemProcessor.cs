using System;
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
        protected abstract Func<TImportObj, string> IdStringFromImportObj { get; }
        protected abstract string DefaultLocation { get; }
        protected virtual string ItemLocation =>
            this.LocationPathOverride
            ?? this.DefaultLocation;

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
            var newId = this.CalculateItemId(importObj);
            var itemLocationInTargetContext = pathOverride ?? this.CalculateItemLocation(importObj);
            var defaultItem = this.GetItem(newId, importObj, itemLocationInTargetContext, defaultLanguage)
                              ?? this.CreateItem(importObj, itemLocationInTargetContext, defaultLanguage);
            return null;
        }

        private object CreateItem(TImportObj importObj, string itemLocationInTargetContext, Language defaultLanguage)
        {
            throw new NotImplementedException();
        }

        private object GetItem(string newId, TImportObj importObj, string itemLocationInTargetContext, Language defaultLanguage)
        {
            throw new NotImplementedException();
        }

        protected string CalculateItemId(TImportObj importObj)
        {
            return this.IdStringFromImportObj(importObj);
        }
        protected virtual string CalculateItemLocation(TImportObj importObj)
        {
            return this.ItemLocation;
        }
    }
}
