namespace TheBookCave.Models.InputModels
{
    public class CartInputModel
    {
        public int BookID { get; set; }
        public string UserID { get; set; }
        public int Amount { get; set; }
        public CartInputModel()
        {
            BookID = 0;
            UserID = null;
            Amount = 0;
        }
        public CartInputModel(int bookID ,string userID, int amount)
        {
            BookID = bookID;
            UserID = userID;
            Amount = amount;
        }
    }
}