using TheBookCave.Models;

namespace TheBookCave.Data.EntityModels
{
    public class UserEntityModel
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public AddressModel Address { get; set; }
        public string Image { get; set; }
        public int? FavoriteBookID { get; set; }
        public bool IsPremium { get; set; }
        public bool IsEmployee { get; set; }
    }
}