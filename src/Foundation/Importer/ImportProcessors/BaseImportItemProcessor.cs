using Glass.Mapper.Sc;
using Importer.Models;
using Importer.Repositories;
using Importer.Search;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.Data.Managers;
using Sitecore.DependencyInjection;
using Sitecore.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using Importer.Enums;

namespace Importer.ImportProcessors
{
    public abstract class BaseImportItemProcessor<TItem, TImportObj> : IBaseImportItemProcessor<TItem, TImportObj>
        where TItem : GlassBase, IGlassBase
    {
        protected ISitecoreContext Context { get; }
        protected readonly string LocationPathOverride;
        protected virtual bool CacheItems => true;
        protected IDictionary<string, IList<TItem>> ItemsCache;
        protected IGenericSitecoreItemRepository<TItem> GenericItemRepository { get; }
        protected abstract Func<TImportObj, string> IdStringFromImportObj { get; }
        protected abstract Func<TItem, string> IdStringFromSitecoreItem { get; }
        protected abstract string DefaultLocation { get; }
        protected virtual bool MapDatabaseFields => false;

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

        public virtual TItem ProcessItem(TImportObj importObj, IEnumerable<Language> languageVersions,
            string pathOverride = null)
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

        public virtual TItem GetItem(
            string targetIdString,
            TImportObj importObj = default(TImportObj),
            string locationOverride = null,
            Language language = null,
            Func<TItem, bool> defaultItemSelector = null)
        {
            if (string.IsNullOrEmpty(targetIdString)) return null;
            var itemLocation = locationOverride ?? this.CalculateItemLocation(importObj);
            var items = this.GetItems(itemLocation, language);
            var matchedItems = items.Where(x =>
                    targetIdString.Equals(this.CalculateItemId(x), StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (!matchedItems.Any() && defaultItemSelector != null)
            {
                matchedItems = items.Where(defaultItemSelector).ToList();
            }

            if (matchedItems.Count > 1)
            {
                throw new ImportLogException
                {
                    Entry = new ImportLogEntry
                    {
                        Level = MessageLevel.Error,
                        Message =
                            $"{this.GetType()}: Multiple matches have been found in \"{itemLocation}\" by Id=\"{targetIdString}\""
                    }
                };
            }

            return matchedItems.FirstOrDefault();
        }

        public virtual IEnumerable<TItem> GetItems(string rootPath, Language language = null)
        {
            var items = this.GenericItemRepository.GetByPath(rootPath, language ?? Language.Current, MapDatabaseFields);
            return items;
        }

        protected string CalculateItemId(TItem item)
        {
            return this.IdStringFromSitecoreItem(item);
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