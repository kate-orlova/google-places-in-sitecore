using Newtonsoft.Json;

namespace GooglePlacesImport.Models
{
    public class Candidate
    {
        [JsonProperty("place_id", NullValueHandling = NullValueHandling.Ignore)]
        public string PlaceId { get; set; }

        [JsonProperty("permanently_close")]
        public bool PermanentlyClosed { get; set; }
    }
}