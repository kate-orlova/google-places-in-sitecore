using System.Collections.Generic;
using Glass.Mapper.Sc;
using Importer.Models;
using Sitecore.DependencyInjection;
using Sitecore.Globalization;

namespace Importer.ImportProcessors
{
    public abstract class BaseImportItemProcessor<TItem, TImportObj> : IBaseImportItemProcessor<TItem, TImportObj> where TItem : GlassBase, IGlassBase
    {
        protected ISitecoreContext Context { get; }
        protected readonly string LocationPathOverride;
        protected virtual bool CacheItems => true;
        protected IDictionary<string, IList<TItem>> ItemsCache;

        protected BaseImportItemProcessor(ISitecoreContext sitecoreContext, string locationPathOverride = null)
        {
            this.Context = sitecoreContext;
            this.LocationPathOverride = locationPathOverride;
            this.ItemsCache = new Dictionary<string, IList<TItem>>();
        }

        public TItem ProcessItem(TImportObj importObj, IEnumerable<Language> languageVersions, string pathOverride = null)
        {
            throw new System.NotImplementedException();
        }
    }
}
