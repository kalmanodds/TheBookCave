namespace TheBookCave.Models.InputModels
{
    public class CartInputModel
    {
        public int BookID { get; set; }
        public string UserID { get; set; }
        public CartInputModel(int bookID, string userID)
        {
            BookID = bookID;
            UserID = userID;
        }
    }
}