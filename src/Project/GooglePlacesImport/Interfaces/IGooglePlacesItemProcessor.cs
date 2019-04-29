using Importer.ImportProcessors;
using Importer.Models;
using Sitecore.Globalization;
using System.Collections.Generic;

namespace GooglePlacesImport.Interfaces
{
    public interface IGooglePlacesItemProcessor : IBaseImportItemProcessor<Item, ItemDto>
    {
        IEnumerable<Item> GetExistItems(Language language = null);
    }
}