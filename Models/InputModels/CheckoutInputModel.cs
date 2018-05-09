namespace TheBookCave.Models.InputModels
{
    public class CheckoutInputModel
    {
        public string CardType { get; set; }
        public string CardHolder { get; set; }
        public string CardNumber { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        public string CVC { get; set; }
        public string FullName { get; set; }
        public string StreetName { get; set; }
        public int? HouseNumber { get; set; }
        public string City { get; set; }
        public int? Zip { get; set; }
        public string Country { get; set; }
    }
}