namespace TheBookCave.Models.InputModels
{
    public class RatingInputModel
    {
        public int Score { get; set; }
        public string Comment { get; set; }
        public string UserID { get; set; }
        public int BookID { get; set; }
    }
}