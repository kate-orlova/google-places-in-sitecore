﻿using Newtonsoft.Json;

namespace GooglePlacesImport.Models
{
    public class Close
    {
        [JsonProperty("day", NullValueHandling = NullValueHandling.Ignore)]
        public long? Day { get; set; }

        [JsonProperty("time", NullValueHandling = NullValueHandling.Ignore)]
        public string Time { get; set; }
    }
}