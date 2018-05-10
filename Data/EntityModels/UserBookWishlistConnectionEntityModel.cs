namespace TheBookCave.Data.EntityModels
{
    public class UserBookWishlistConnectionEntityModel
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public int BookID { get; set; }
        public UserBookWishlistConnectionEntityModel()
        {
            UserID = "";
            BookID = 0;
        }
        public UserBookWishlistConnectionEntityModel(string userID, int bookID)
        {
            UserID = userID;
            BookID = bookID;
        }
    }
}