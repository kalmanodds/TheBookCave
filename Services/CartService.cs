using System.Collections.Generic;
using TheBookCave.Models.InputModels;
using TheBookCave.Models.ViewModels;
using TheBookCave.Repositories;

namespace TheBookCave.Services
{
    public class CartService
    {
        private CartRepo _cartRepo;

        public CartService()
        {
            _cartRepo = new CartRepo();
        }

        public List<BookViewModel> GetCartBooks(string userID)
        {
            return _cartRepo.GetCartBooks(userID);
        }

        public void AddCartItem(CartInputModel model)
        {
            _cartRepo.AddCartItem(model);
        }
    }
}