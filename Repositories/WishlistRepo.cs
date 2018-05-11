using System.Linq;
using TheBookCave.Data;
using TheBookCave.Data.EntityModels;
using TheBookCave.Models.InputModels;

namespace TheBookCave.Repositories
{
    public class WishlistRepo
    {
        //Private member variable to manipulate database.
        private DataContext _db;

        //Constructor that initializes database.
        public WishlistRepo()
        {
            _db = new DataContext();
        }

        //Adds item to wishlist
        public void AddWishlistItem(WishlistInputModel model)
        {
            var connection = (from w in _db.UserBookWishlistConnections
                              where w.UserID == model.UserID && w.BookID == model.BookID
                              select w).FirstOrDefault();
            if(connection == null)
            {
                var newWishlistItem = new UserBookWishlistConnectionEntityModel(model.UserID, model.BookID);
                _db.Add(newWishlistItem);
                _db.SaveChanges();
            }
        }

        //Removes the wishlist item.
        public void RemoveItem(string userID, int bookID)
        {
            var connection = (from c in _db.UserBookWishlistConnections
                              where c.BookID == bookID && c.UserID == userID
                              select c).FirstOrDefault();
            
            if(connection != null)
            {
                _db.UserBookWishlistConnections.Remove(connection);
                _db.SaveChanges();
            }
        }
    }
}