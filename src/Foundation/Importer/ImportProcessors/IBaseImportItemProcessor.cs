using System.Collections.Generic;
using Importer.Models;
using Sitecore.Globalization;

namespace Importer.ImportProcessors
{
    public interface IBaseImportItemProcessor<out TItem, TImportObj> where TItem : IGlassBase
    {
        TItem ProcessItem(TImportObj importObj, IEnumerable<Language> languageVersions = null,
            string pathOverride = null);
    }
}