using Sitecore.Globalization;
using System.Collections.Generic;
using Importer.ImportProcessors;
using Importer.Models;

namespace GooglePlacesImport.Interfaces
{
    public interface IGooglePlacesItemProcessor : IBaseImportItemProcessor<Item, ItemDto>
    {
        IEnumerable<Item> GetExistItems(Language language = null);
    }
}