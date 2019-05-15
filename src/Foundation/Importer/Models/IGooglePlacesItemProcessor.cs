using Importer.ImportProcessors;
using Sitecore.Globalization;
using System.Collections.Generic;

namespace Importer.Models
{
    public interface IGooglePlacesItemProcessor : IBaseImportItemProcessor<Item, ItemDto>
    {
        IEnumerable<Item> GetExistItems(Language language = null);
    }
}
