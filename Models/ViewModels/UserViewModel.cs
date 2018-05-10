namespace TheBookCave.Models.ViewModels
{
    public class UserViewModel
    {
        public string UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public AddressModel Address { get; set; }
        public string Image { get; set; }
        public string FavoriteBookImage { get; set; }
        public bool IsPremium { get; set; }
    }
}