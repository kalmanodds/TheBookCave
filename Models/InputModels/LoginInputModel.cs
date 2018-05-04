using System.ComponentModel.DataAnnotations;

namespace TheBookCave.Models.InputModels
{
    public class LoginInputModel
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public bool RememberMe { get; set; }
    }
}