﻿using Newtonsoft.Json;

namespace GooglePlacesImport.Models
{
    public partial class Result
    {
        [JsonProperty("formatted_address", NullValueHandling = NullValueHandling.Ignore)]
        public string FormattedAddress { get; set; }
    }
}