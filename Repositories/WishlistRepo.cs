using TheBookCave.Data;
using TheBookCave.Data.EntityModels;
using TheBookCave.Models.InputModels;

namespace TheBookCave.Repositories
{
    public class WishlistRepo
    {
        private DataContext _db;

        public WishlistRepo()
        {
            _db = new DataContext();
        }

        public void AddWishlistItem(WishlistInputModel model)
        {
            var newWishlistItem = new UserBookWishlistConnectionEntityModel(model.UserID, model.BookID);
            _db.Add(newWishlistItem);
            _db.SaveChanges();
        }
    }
}