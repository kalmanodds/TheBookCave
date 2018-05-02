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

    //Möguleg föll sem við munum útfæra:
        //getRatings()
        //getRating()
        //AddRating()
        //UpdateRating()
        //RemoveRating()
    }
}