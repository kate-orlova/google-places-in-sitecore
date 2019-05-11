using Newtonsoft.Json;

namespace GooglePlacesImport.Models
{
    public partial class Period
    {
        [JsonProperty("close", NullValueHandling = NullValueHandling.Ignore)]
        public Close Close { get; set; }
    }
}