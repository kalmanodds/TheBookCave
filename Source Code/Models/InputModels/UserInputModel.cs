namespace TheBookCave.Models.InputModels
{
    public class UserInputModel
    {
        public string UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public AddressModel Address { get; set; }
        public string Image { get; set; }
    }
}