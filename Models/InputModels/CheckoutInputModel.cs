using System.ComponentModel.DataAnnotations;

namespace TheBookCave.Models.InputModels
{
    public class CheckoutInputModel
    {
        [Required(ErrorMessage = "You must choose a payment option")]
        public string CardType { get; set; }
        [Required(ErrorMessage = "Cardholders name is required")]
        public string CardHolder { get; set; }
        [Required(ErrorMessage = "Card number is required")]
        public string CardNumber { get; set; }
        [Required(ErrorMessage = "Month is required")]
        public int? Month { get; set; }
        [Required(ErrorMessage = "Year is required")]
        public int? Year { get; set; }
        [Required(ErrorMessage = "CVC is required")]
        public string CVC { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Street name is required")]
        public string StreetName { get; set; }
        [Required(ErrorMessage = "House number is required")]
        public int? HouseNumber { get; set; }
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }
        [Required(ErrorMessage = "Zip code is required")]
        public int Zip { get; set; }
        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; }

        public string UserID { get; set; }
    }
}