using System.Collections.Generic;
using Newtonsoft.Json;

namespace GooglePlacesImport.Models
{
    public class GooglePlacesGetByIdResponse
    {
        [JsonProperty("html_attributions", NullValueHandling = NullValueHandling.Ignore)]
        public List<object> HtmlAttributions { get; set; }

        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }

        [JsonProperty("result", NullValueHandling = NullValueHandling.Ignore)]
        public Result Result { get; set; }
    }
}