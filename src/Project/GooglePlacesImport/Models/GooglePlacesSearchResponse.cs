using System.Collections.Generic;
using Newtonsoft.Json;

namespace GooglePlacesImport.Models
{
    public class GooglePlacesSearchResponse
    {
        [JsonProperty("candidates", NullValueHandling = NullValueHandling.Ignore)]
        public List<Candidate> Candidates { get; set; }

        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }
    }
}