using System.Collections.Generic;
using System.Linq;
using TheBookCave.Data;
using TheBookCave.Data.EntityModels;
using TheBookCave.Models.ViewModels;

namespace TheBookCave.Repositories
{
    public class OrderBookConnectionRepo
    {
        private DataContext _db;

        public OrderBookConnectionRepo()
        {
            _db = new DataContext();
        }

        public void AddConnection(int orderID, int bookID, int amount)
        {
            var newConnection = new OrderBookConnectionEntityModel()
            {
                OrderID = orderID,
                BookID = bookID,
                Amount = amount,
            };
            _db.Add(newConnection);
            _db.SaveChanges();
        }

        public void UpdateConnection(int orderID, int bookID, int amount)
        {
            var connection = (from c in _db.OrderBookConnections
                              where c.BookID == bookID && c.OrderID == orderID
                              select c).FirstOrDefault();

            if(connection != null)
            {
                connection.Amount = amount;
                _db.Update(connection);
                _db.SaveChanges();
            }
            else
            {
                AddConnection(orderID, bookID, amount);
            }
        }

        public void RemoveItem(int orderID, int bookID)
        {
            var connection = (from c in _db.OrderBookConnections
                            where c.BookID == bookID && c.OrderID == orderID
                            select c).FirstOrDefault();

            if(connection == null)
            {
                return;
            }
            
            _db.OrderBookConnections.Remove(connection);
            _db.SaveChanges();
        }

        public List<BookViewModel> GetBooks(int orderID)
        {
            var books = (from b in _db.Books
                         join c in _db.OrderBookConnections on b.ID equals c.BookID
                         where c.OrderID == orderID
                         select new BookViewModel()
                         {
                             ID = b.ID,
                             Title = b.Title,
                             Author = b.Author,
                             Description = b.Description,
                             Price = b.Price,
                             Genre = b.Genre,
                             NumberOfPages = b.NumberOfPages,
                             NumberOfCopiesSold = b.NumberOfCopiesSold,
                             DatePublished = b.DatePublished,
                             Publisher = b.Publisher,
                             Image = b.Image,
                             Amount = c.Amount,
                         }).ToList();

            if(books != null)
            {
                return books;
            }
            return null;
        }
    }
}