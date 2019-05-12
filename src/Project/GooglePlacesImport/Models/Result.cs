using Newtonsoft.Json;

namespace GooglePlacesImport.Models
{
    public partial class Result
    {
        [JsonProperty("formatted_address", NullValueHandling = NullValueHandling.Ignore)]
        public string FormattedAddress { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
        public string Url { get; set; }

        [JsonProperty("formatted_phone_number", NullValueHandling = NullValueHandling.Ignore)]
        public string FormattedPhoneNumber { get; set; }

        [JsonProperty("opening_hours", NullValueHandling = NullValueHandling.Ignore)]
        public OpeningHours OpeningHours { get; set; }

        [JsonProperty("rating", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Rating { get; set; }
    }
}