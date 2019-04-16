using System;

namespace Importer.Models
{
    public class GooglePlaceDto
    {
        public string PlaceId { get; set; }
        public string BasicFormattedAddress { get; set; }
        public string BasicName { get; set; }
        public string BasicUrl { get; set; }
        public DateTime BasicDataImported { get; set; }
    }
}
