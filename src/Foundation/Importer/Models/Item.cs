using System;

namespace Importer.Models
{
    public class Item : GlassBase, IGlassBase
    {
        public string PlaceId { get; set; }
        public string BasicFormattedAddress { get; set; }
        public string BasicName { get; set; }
        public string BasicUrl { get; set; }
        public DateTime BasicDataImported { get; set; }
        public string ContactFormattedPhoneNumber { get; set; }
        public string ContactOpeningHours { get; set; }
        public DateTime ContactDataImported { get; set; }
        public decimal AtmosphereRating { get; set; }
        public DateTime AtmosphereDataImported { get; set; }
    }
}