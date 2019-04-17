using System.Collections.Generic;
using Importer.Models;
using Sitecore.Globalization;

namespace Importer.ImportProcessors
{
    public abstract class BaseImportItemProcessor<TItem, TImportObj> : IBaseImportItemProcessor<TItem, TImportObj> where TItem : GlassBase, IGlassBase
    {
        public TItem ProcessItem(TImportObj importObj, IEnumerable<Language> languageVersions, string pathOverride = null)
        {
            throw new System.NotImplementedException();
        }
    }
}
