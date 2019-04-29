using Importer.Models;
using System.Collections.Generic;

namespace GooglePlacesImport.Interfaces
{
    public interface IGooglePlacesService
    {
        IEnumerable<ItemDto> PopulateGooglePlacesIds(IEnumerable<ItemDto> existingStockists, bool reSearchPlaceId, ref List<ImportLogEntry> logEntries);
        IEnumerable<ItemDto> PopulateGooglePlacesData(IEnumerable<ItemDto> existingStockists, ref List<ImportLogEntry> logEntries);

    }
}
