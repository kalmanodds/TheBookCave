using System.Collections.Generic;
using System.Linq;
using TheBookCave.Data;
using TheBookCave.Data.EntityModels;
using TheBookCave.Models.InputModels;
using TheBookCave.Models.ViewModels;

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
            //Test if Connection is made.
            var allConnections = (from c in _db.UserBookCartConnections
                                  where c.UserID == model.UserID
                                  select c);

            int amountInCart = 0;
            for(int i = 0; i < allConnections.Count(); i++)
            {
                amountInCart += allConnections.ToList().ElementAt(i).Amount;
            }

            if(amountInCart >= 64)
            {
                //Cant add if cart is more than 64
                return;
            }

            var connection = allConnections.Where(c => c.BookID == model.BookID).FirstOrDefault();

            //If connection is established, increase the amount.
            if(connection != null)
            {
                connection.Amount++;
                _db.Update(connection);
                _db.SaveChanges();
            }
            //If connection is not established, it will be formed.
            else
            {
                var newCartItem = new UserBookCartConnectionEntityModel()
                {
                    UserID = model.UserID,
                    BookID = model.BookID,
                    Amount = 1,
                };
                _db.Add(newCartItem);
                _db.SaveChanges();
            }
        }

        public List<BookViewModel> GetCartBooks(string userID)
        {
            var books = (from b in _db.Books
                         join c in _db.UserBookCartConnections on b.ID equals c.BookID
                         where c.UserID == userID
                         select new BookViewModel()
                         {
                            ID = b.ID,
                            Title = b.Title,
                            Author = b.Author,
                            Description = b.Description,
                            Price = b.Price,
                            NumberOfPages = b.NumberOfPages,
                            NumberOfCopiesSold = b.NumberOfCopiesSold,
                            DatePublished = b.DatePublished,
                            Publisher = b.Publisher,
                            Rating = b.Rating,
                            Image = b.Image,
                            Amount = c.Amount,
                         }
                        ).ToList();

            return books;
        }

        public void UpdateConnection(CartInputModel model)
        {
            var connection = (from c in _db.UserBookCartConnections
                              where c.BookID == model.BookID && c.UserID == model.UserID
                              select c).FirstOrDefault();
            if(model.Amount > 0)
            {
                connection.Amount = model.Amount;
                _db.UserBookCartConnections.Update(connection);
                _db.SaveChanges();
            }
            else
            {
                //NOT ALLOWED TO PUT BOOKS BELOW ZERO
            }
        }

        public void RemoveItem(string userID, int bookID)
        {
            var connection = (from c in _db.UserBookCartConnections
                              where c.UserID == userID && c.BookID == bookID
                              select c).FirstOrDefault();
            
            _db.UserBookCartConnections.Remove(connection);
            _db.SaveChanges();
        }
    }
}