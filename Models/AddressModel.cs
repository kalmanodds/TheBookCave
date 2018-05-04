namespace TheBookCave.Models
{
    public class AddressModel
    {
        public int ID { get; set; }
        public string StreetName { get; set; }
        public int HouseNumber { get; set; }
        public string City { get; set; }
        public int Zip { get; set; }
        public string Country { get; set; }
    }
}