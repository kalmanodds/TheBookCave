namespace TheBookCave.Data.EntityModels
{
    public class UserBookCartConnectionEntityModel
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public int BookID { get; set; }
        public UserBookCartConnectionEntityModel(string userID, int bookID)
        {
            UserID = userID;
            BookID = bookID;
        }
    }
}