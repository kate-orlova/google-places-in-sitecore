using System.Collections.Generic;
using Newtonsoft.Json;

namespace GooglePlacesImport.Models
{
    public class DebugLog
    {
        [JsonProperty("line", NullValueHandling = NullValueHandling.Ignore)]
        public List<object> Line { get; set; }
    }
}