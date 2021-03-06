﻿using Newtonsoft.Json;

namespace GooglePlacesImport.Models
{
    public class Period
    {
        [JsonProperty("close", NullValueHandling = NullValueHandling.Ignore)]
        public Close Close { get; set; }

        [JsonProperty("open", NullValueHandling = NullValueHandling.Ignore)]
        public Close Open { get; set; }
    }
}