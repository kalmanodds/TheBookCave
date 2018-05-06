using TheBookCave.Models.InputModels;
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

        public void AddCartItem(CartInputModel model)
        {
            _cartRepo.AddCartItem(model);
        }
    }
}