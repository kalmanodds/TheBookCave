namespace TheBookCave.Models.ViewModels
{
    public class RatingViewModel
    {
        public int RatingID { get; set; }
        public int Score { get; set; }
        public string Comment { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Image { get; set; }
        public int Votes { get; set; }
    }
}
