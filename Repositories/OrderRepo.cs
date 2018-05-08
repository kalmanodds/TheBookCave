using System.Linq;
using TheBookCave.Data;
using TheBookCave.Data.EntityModels;
using TheBookCave.Models;
using TheBookCave.Models.InputModels;

namespace TheBookCave.Repositories
{
    public class OrderRepo
    {
        private DataContext _db;

        public OrderRepo()
        {
            _db = new DataContext();
        }

        public void AddOrder(OrderInputModel model, double totalPrice, DateModel dateOrdered)
        {
            var newOrder = new OrderEntityModel()
            {
                UserID = model.UserID,
                TotalPrice = totalPrice,
                DateOrder = dateOrdered,
                CurrentOrder = true,
            };
            _db.Add(newOrder);
            _db.SaveChanges();
        }

        public bool UserHasCurrentOrder(string userID)
        {
            var order = (from o in _db.Orders
                       where o.UserID == userID && o.CurrentOrder == true
                       select o).FirstOrDefault();
            if(order != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int? GetCurrentOrderID(string userID)
        {
            var order = (from o in _db.Orders
                         where o.UserID == userID && o.CurrentOrder == true
                         select o).SingleOrDefault();
            if(order != null)
            {
                return order.ID;
            }
            else
            {
                return null;
            }
        }

        public void UpdateOrder(OrderInputModel model, double totalPrice, DateModel dateOrdered, int orderID)
        {
            var order = (from o in _db.Orders
                         where o.ID == orderID
                         select o).FirstOrDefault();

            order.UserID = model.UserID;
            order.TotalPrice = totalPrice;
            order.DateOrder = dateOrdered;
            order.CurrentOrder = true;

            _db.Orders.Update(order);
            _db.SaveChanges();
        }
    }
}