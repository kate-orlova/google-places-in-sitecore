namespace Importer.Models
{
    public class ItemDto
    {
        public string PlaceId { get; set; }
        public GooglePlaceDto GooglePlaceData { get; set; }
        public string CompanyName { get; set; }
        public string AddressLine1 { get; set; }
        public string City { get; set; }
    }
}
