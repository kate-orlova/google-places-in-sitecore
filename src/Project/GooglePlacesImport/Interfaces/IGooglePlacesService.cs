using Importer.Models;
using System.Collections.Generic;

namespace GooglePlacesImport.Interfaces
{
    public interface IGooglePlacesService
    {
        IEnumerable<ItemDto> PopulateGooglePlacesIds(IEnumerable<ItemDto> existingItems, bool reSearchPlaceId,
            ref List<ImportLogEntry> logEntries);

        IEnumerable<ItemDto> PopulateGooglePlacesData(IEnumerable<ItemDto> existingItems,
            ref List<ImportLogEntry> logEntries);
    }
}