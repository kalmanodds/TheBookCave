using System.Collections.Generic;
using System.Linq;
using TheBookCave.Data;
using TheBookCave.Data.EntityModels;
using TheBookCave.Models.ViewModels;

namespace TheBookCave.Repositories
{
    public class OrderBookConnectionRepo
    {
        //Private member variable to manipulate database.
        private DataContext _db;

        //Constructor that initialzes Database.
        public OrderBookConnectionRepo()
        {
            _db = new DataContext();
        }

        //Creates the connection between book and order.
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

        //Updates connection in order.
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

        //Removes connection in order if item is removed from cart.
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

        //Gets all books in order.
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