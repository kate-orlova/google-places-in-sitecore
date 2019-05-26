using System.Collections.Generic;
using Newtonsoft.Json;

namespace GooglePlacesImport.Models
{
    public class OpeningHours
    {
        [JsonProperty("open_now", NullValueHandling = NullValueHandling.Ignore)]
        public bool? OpenNow { get; set; }

        [JsonProperty("periods", NullValueHandling = NullValueHandling.Ignore)]
        public List<Period> Periods { get; set; }

        [JsonProperty("weekday_text", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> WeekdayText { get; set; }
    }
}