using GooglePlacesImport.Interfaces;
using Importer.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GooglePlacesImport.Services
{
    public class GooglePlacesService : IGooglePlacesService
    {
        public IEnumerable<ItemDto> PopulateGooglePlacesIds(IEnumerable<ItemDto> existingItems, bool reSearchPlaceId, ref List<ImportLogEntry> logEntries)
        {
            ConcurrentBag<ItemDto> items;
            IEnumerable<ItemDto> itemsToSearchPlaceId;
            var logs = new ConcurrentBag<ImportLogEntry>();

            if (reSearchPlaceId)
            {
                itemsToSearchPlaceId = existingItems;
                items = new ConcurrentBag<ItemDto>();
            }
            else
            {
                itemsToSearchPlaceId = existingItems.Where(x =>
                    x.GooglePlaceData == null || string.IsNullOrWhiteSpace(x.GooglePlaceData.PlaceId));
                items = new ConcurrentBag<ItemDto>(existingItems.Where(x =>
                    x.GooglePlaceData != null && !string.IsNullOrWhiteSpace(x.GooglePlaceData.PlaceId)));
            }

            Parallel.ForEach(itemsToSearchPlaceId, item =>
            {
                if (item.GooglePlaceData == null)
                {
                    item.GooglePlaceData = new GooglePlaceDto();
                }
            });

            return items;
        }

        public IEnumerable<ItemDto> PopulateGooglePlacesData(IEnumerable<ItemDto> existingItems, ref List<ImportLogEntry> logEntries)
        {
            throw new NotImplementedException();
        }
    }
}