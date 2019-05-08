using GooglePlacesImport.Interfaces;
using Importer.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using GooglePlacesImport.Models;
using Importer.Enums;
using log4net;
using Newtonsoft.Json;

namespace GooglePlacesImport.Services
{
    public class GooglePlacesService : IGooglePlacesService
    {
        protected readonly ILog Logger;

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

            try
            {
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
                    try
                    {
                        var responseStream = request.GetResponse().GetResponseStream();
                        string responseText;
                        using (var sr = new StreamReader(responseStream))
                        {
                            responseText = sr.ReadToEnd();
                        }

                        var searchResults = JsonConvert.DeserializeObject<GooglePlacesSearchResponse>(responseText);
                        if (searchResults.Candidates != null &&
                            searchResults.Candidates.Count(x => !x.PermanentlyClosed) == 1)
                        {
                            item.GooglePlaceData.PlaceId =
                                searchResults.Candidates.First(x => !x.PermanentlyClosed).PlaceId;
                            items.Add(item);
                        }
                        else
                        {
                            if (searchResults.Candidates != null &&
                                searchResults.Candidates.Count(x => !x.PermanentlyClosed) > 1)
                            {
                                var logEntry = new ImportLogEntry
                                {
                                    Message =
                                        $"{item.CompanyName} - Multiple Google Place IDs found: {string.Join(", ", searchResults.Candidates.Select(x => x.PlaceId))}\nRequest: {requestUrl}",
                                    Action = ImportAction.Rejected,
                                    Level = MessageLevel.Info
                                };
                                this.Logger.Info(logEntry.Message);
                                logs.Add(logEntry);
                            }
                            else if (searchResults.Candidates != null &&
                                     searchResults.Candidates.Any(x => x.PermanentlyClosed))
                            {
                                var logEntry = new ImportLogEntry
                                {
                                    Message =
                                        $"{item.CompanyName} - Google Place is permanently closed\nRequest: {requestUrl}",
                                    Action = ImportAction.Rejected,
                                    Level = MessageLevel.Info
                                };
                                this.Logger.Info(logEntry.Message);
                                logs.Add(logEntry);
                            }
                            else
                            {
                                var logEntry = new ImportLogEntry
                                {
                                    Message = $"{item.CompanyName} - No Google Place IDs found\nRequest: {requestUrl}",
                                    Action = ImportAction.Rejected,
                                    Level = MessageLevel.Info
                                };
                                this.Logger.Info(logEntry.Message);
                                logs.Add(logEntry);
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        var logEntry = new ImportLogEntry
                        {
                            Message =
                                $"{item.CompanyName} - error while searching by Google Place ID\nRequest: {requestUrl}",
                            Action = ImportAction.Rejected,
                            Level = MessageLevel.Info
                        };
                        this.Logger.Error(logEntry.Message, exception);
                        logs.Add(logEntry);
                    }
                });
            }
            catch (Exception exception)
            {
                var logEntry = new ImportLogEntry
                {
                    Message = $"Error searching Google Place IDs",
                    Action = ImportAction.Undefined,
                    Level = MessageLevel.Error
                };
                this.Logger.Error(logEntry.Message, exception);
                logs.Add(logEntry);
            }

            logEntries.AddRange(logs);

            return items;
        }

        public IEnumerable<ItemDto> PopulateGooglePlacesData(IEnumerable<ItemDto> existingItems, ref List<ImportLogEntry> logEntries)
        {
            throw new NotImplementedException();
        }
    }
}