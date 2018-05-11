using System.Collections.Generic;
using System.Linq;
using TheBookCave.Data;
using TheBookCave.Data.EntityModels;
using TheBookCave.Models.InputModels;
using TheBookCave.Models.ViewModels;

namespace TheBookCave.Repositories
{
    public class RatingRepo
    {
        private DataContext _db;

        public RatingRepo()
        {
            _db = new DataContext();
        }

        public void AddRating(RatingInputModel model)
        {
            var rating = (from r in _db.Ratings
                          where r.UserID == model.UserID && r.BookID == model.BookID
                          select r).FirstOrDefault();
            
            if(rating == null)
            {
                var newRating = new RatingEntityModel()
                {
                    Score = model.Score,
                    Comment = model.Comment,
                    UserID = model.UserID,
                    BookID = model.BookID,
                    Votes = 1,
                };

                _db.Ratings.Add(newRating);
                _db.SaveChanges();
            }
        }

        public List<RatingViewModel> GetRatings(int bookID)
        {
            var ratings = (from r in _db.Ratings
                           join u in _db.Users on r.UserID equals u.UserID
                           where r.BookID == bookID
                           select new RatingViewModel()
                            {
                                RatingID = r.ID,
                                Score = r.Score,
                                Comment = r.Comment,
                                FirstName = u.FirstName,
                                LastName = u.LastName,
                                Image = u.Image,
                                Votes = r.Votes,
                            }
                           ).ToList();

            return ratings;
        }

        public void AddVote(string userID, int ratingID)
        {
            var connection = (from c in _db.UserRatingVoteConnections
                              where c.UserID == userID && c.RatingID == ratingID
                              select c).FirstOrDefault();

            if(connection == null)
            {
                var newConnection = new UserRatingVoteConnectionEntityModel()
                {
                    UserID = userID,
                    RatingID = ratingID,
                };

                _db.UserRatingVoteConnections.Add(newConnection);
                _db.SaveChanges();

                var rating = (from r in _db.Ratings
                              where r.ID == ratingID
                              select r).FirstOrDefault();
                rating.Votes++;
                
                _db.Ratings.Update(rating);
                _db.SaveChanges();
            }
        }

        public void RemoveRating(int ratingID)
        {
            var rating = (from r in _db.Ratings
                          where r.ID == ratingID
                          select r).FirstOrDefault();

            _db.Remove(rating);
            _db.SaveChanges();

            var connections = (from r in _db.UserRatingVoteConnections
                              where r.RatingID == ratingID
                              select r).ToList();
            _db.RemoveRange(connections);
            _db.SaveChanges();
        }
    }
}