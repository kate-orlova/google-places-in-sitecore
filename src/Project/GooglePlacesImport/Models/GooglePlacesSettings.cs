using Importer.Models;

namespace GooglePlacesImport.Models
{
    public class GooglePlacesSettings : GlassBase
    {
        public string GooglePlaceSearchRequest { get; set; }
        public string GooglePlaceDetailsByIdRequest { get; set; }

        public string GooglePlaceBasicDataFields { get; set; }
    }
}