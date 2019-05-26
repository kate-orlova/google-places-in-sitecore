
using Sitecore.Globalization;
using System.Collections.Generic;
using Importer.Models;

namespace Importer.ImportProcessors
{
    public interface IGooglePlacesItemProcessor : IBaseImportItemProcessor<Item, ItemDto>
    {
        IEnumerable<Item> GetExistItems(Language language = null);
    }
}