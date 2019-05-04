using GooglePlacesImport.Interfaces;
using Importer.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace GooglePlacesImport.Services
{
    public class GooglePlacesService : IGooglePlacesService
    {
        public IEnumerable<ItemDto> PopulateGooglePlacesIds(IEnumerable<ItemDto> existingItems, bool reSearchPlaceId, ref List<ImportLogEntry> logEntries)
        {
            ConcurrentBag<ItemDto> items;
            IEnumerable<ItemDto> itemsToSearchPlaceId;
            var logs = new ConcurrentBag<ImportLogEntry>();
            var baseUrl = "BASE_URL";
            var key = "GOOGLE_API_KEY";


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
                var searchString = HttpUtility.UrlEncode(
                    $"{item.CompanyName} {item.AddressLine1} {item.City} {item.County} {item.Postcode}");
                var requestUrl = string.Format(baseUrl, key, searchString,
                    item.Latitude.ToString(CultureInfo.InvariantCulture),
                    item.Longitude.ToString(CultureInfo.InvariantCulture));

                var request = WebRequest.Create(requestUrl);
            });

            return items;
        }

        public IEnumerable<ItemDto> PopulateGooglePlacesData(IEnumerable<ItemDto> existingItems, ref List<ImportLogEntry> logEntries)
        {
            throw new NotImplementedException();
        }
    }
}