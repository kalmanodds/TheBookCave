namespace TheBookCave.Models.InputModels
{
    public class WishlistInputModel
    {
        public int BookID { get; set; }
        public string UserID { get; set; }
        public WishlistInputModel(int bookID, string userID)
        {
            BookID = bookID;
            UserID = userID;
        }
    }
}