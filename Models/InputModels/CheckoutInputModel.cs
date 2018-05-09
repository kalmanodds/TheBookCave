using System.ComponentModel.DataAnnotations;

namespace TheBookCave.Models.InputModels
{
    public class CheckoutInputModel
    {
        [Required]
        public string CardType { get; set; }
        [Required]
        public string CardHolder { get; set; }
        [Required]
        public string CardNumber { get; set; }
        [Required]
        public int? Month { get; set; }
        [Required]
        public int? Year { get; set; }
        [Required]
        public string CVC { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string StreetName { get; set; }
        [Required]
        public int? HouseNumber { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public int Zip { get; set; }
        [Required]
        public string Country { get; set; }
    }
}