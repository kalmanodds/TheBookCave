using TheBookCave.Data;
using TheBookCave.Data.EntityModels;
using TheBookCave.Models.InputModels;

namespace TheBookCave.Repositories
{
    public class CartRepo
    {
        private DataContext _db;

        public CartRepo()
        {
            _db = new DataContext();
        }

        public void AddCartItem(CartInputModel model)
        {
            var newCartItem = new UserBookCartConnectionEntityModel(model.UserID, model.BookID);
            _db.Add(newCartItem);
            _db.SaveChanges();
        }
    }
}