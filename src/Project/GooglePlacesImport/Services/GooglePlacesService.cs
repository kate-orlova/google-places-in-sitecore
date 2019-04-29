using GooglePlacesImport.Interfaces;
using Importer.Models;
using System;
using System.Collections.Generic;

namespace GooglePlacesImport.Services
{
    public class GooglePlacesService : IGooglePlacesService
    {
        public IEnumerable<ItemDto> PopulateGooglePlacesIds(IEnumerable<ItemDto> existingStockists, bool reSearchPlaceId, ref List<ImportLogEntry> logEntries)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ItemDto> PopulateGooglePlacesData(IEnumerable<ItemDto> existingStockists, ref List<ImportLogEntry> logEntries)
        {
            throw new NotImplementedException();
        }
    }
}