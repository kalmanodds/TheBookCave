using TheBookCave.Models.InputModels;
using TheBookCave.Repositories;

namespace TheBookCave.Services
{
    public class WishlistService
    {
        private WishlistRepo _wishlistRepo;

        public WishlistService()
        {
            _wishlistRepo = new WishlistRepo();
        }

        public void AddWishlistItem(WishlistInputModel model)
        {
            _wishlistRepo.AddWishlistItem(model);
        }
    }
}