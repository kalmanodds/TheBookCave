using System.Collections.Generic;
using TheBookCave.Models.InputModels;
using TheBookCave.Models.ViewModels;
using TheBookCave.Repositories;

namespace TheBookCave.Services
{
    public class RatingService
    {
        private RatingRepo _ratingRepo;

        public RatingService()
        {
            _ratingRepo = new RatingRepo();
        }

        public void AddRating(RatingInputModel model)
        {
            _ratingRepo.AddRating(model);
        }

        public List<RatingViewModel> GetRatings(int bookID)
        {
            return _ratingRepo.GetRatings(bookID);
        }

        public void AddVote(string userID, int ratingID)
        {
            _ratingRepo.AddVote(userID, ratingID);
        }
    }
}