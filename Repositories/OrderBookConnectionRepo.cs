using System.Linq;
using TheBookCave.Data;
using TheBookCave.Data.EntityModels;

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
    }
}