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
            var baseUrl = "BASE_URL";
            var key = "GOOGLE_API_KEY";
            var items = new ConcurrentBag<ItemDto>();
            var basicFields = "name,url,formatted_address";
            var contactFields = "formatted_phone_number,opening_hours";
            var atmosphereFields = "rating";
            Int32 basicDataCacheMinutes = 10080;
            Int32 contactDataCacheMinutes = 10080;
            Int32 atmosphereDataCacheMinutes = 10080;

            if (basicDataCacheMinutes <= 0 || contactDataCacheMinutes <= 0 || atmosphereDataCacheMinutes <= 0 ||
                string.IsNullOrWhiteSpace(basicFields) || string.IsNullOrWhiteSpace(contactFields) ||
                string.IsNullOrWhiteSpace(atmosphereFields))
            {
                var logEntry = new ImportLogEntry
                {
                    Message = $"Google Places Data import settings are incomplete or empty",
                    Action = ImportAction.Undefined,
                    Level = MessageLevel.Error
                };
                this.Logger.Error(logEntry.Message);
                logEntries.Add(logEntry);
                return new List<ItemDto>();
            }

            Parallel.ForEach(existingItems, item =>
            {
                var fields = new List<string>();
                if (item.GooglePlaceData.BasicDataImported.AddMinutes(basicDataCacheMinutes) < DateTime.Now) fields.Add(basicFields);
                if (item.GooglePlaceData.ContactDataImported.AddMinutes(contactDataCacheMinutes) < DateTime.Now) fields.Add(contactFields);
                if (item.GooglePlaceData.AtmosphereDataImported.AddMinutes(atmosphereDataCacheMinutes) < DateTime.Now) fields.Add(atmosphereFields);

                if (fields.Any())
                {
                    var allFields = string.Join(",", fields);
                    var requestUrl = string.Format(baseUrl, key, item.GooglePlaceData.PlaceId, allFields);
                    var request = WebRequest.Create(requestUrl);

                    var responseStream = request.GetResponse().GetResponseStream();
                    string responseText;

                    using (var sr = new StreamReader(responseStream))
                    {
                        responseText = sr.ReadToEnd();
                    }

                    var placeData = JsonConvert.DeserializeObject<GooglePlacesGetByIdResponse>(responseText);
                    if (fields.Contains(basicFields, StringComparer.OrdinalIgnoreCase))
                    {
                        item.GooglePlaceData.BasicFormattedAddress = placeData?.Result?.FormattedAddress;
                        item.GooglePlaceData.BasicName = placeData?.Result?.Name;
                        item.GooglePlaceData.BasicUrl = placeData?.Result?.Url;
                        item.GooglePlaceData.BasicDataImported = DateTime.Now;
                    }

                    if (fields.Contains(contactFields, StringComparer.OrdinalIgnoreCase))
                    {
                        item.GooglePlaceData.ContactFormattedPhoneNumber = placeData?.Result?.FormattedPhoneNumber;
                        item.GooglePlaceData.ContactDataImported = DateTime.Now;
                    }
                }
            });

            return items;
        }
    }
}