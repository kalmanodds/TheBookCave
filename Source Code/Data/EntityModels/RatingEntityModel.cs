namespace TheBookCave.Data.EntityModels
{
    public class RatingEntityModel
    {
        public int ID { get; set; }
        public int Score { get; set; }
        public string Comment { get; set; }
        public string UserID { get; set; }
        public int BookID { get; set; }
        public int Votes { get; set; }
    }
}
