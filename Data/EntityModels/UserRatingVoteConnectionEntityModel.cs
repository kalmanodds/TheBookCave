namespace TheBookCave.Data.EntityModels
{
    public class UserRatingVoteConnectionEntityModel
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public int RatingID { get; set; }
    }
}