using Newtonsoft.Json;

namespace GooglePlacesImport.Models
{
    public class GooglePlacesSearchResponse
    {
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }
    }
}