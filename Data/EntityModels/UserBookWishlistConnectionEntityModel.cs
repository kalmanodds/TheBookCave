namespace TheBookCave.Data.EntityModels
{
    public class UserBookWishlistConnectionEntityModel
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int BookID { get; set; }
    }
}