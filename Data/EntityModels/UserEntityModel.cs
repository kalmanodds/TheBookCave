using TheBookCave.Models;

namespace TheBookCave.Data.EntityModels.UserEntityModel
{
    public class UserEntityModel
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public AdressModel Address { get; set; }
        public DateModel DateJoined {get; set; }
        public string Image { get; set; }
        public bool IsPremium { get; set; }
        public int FavoriteBookID { get; set; }
    }
}