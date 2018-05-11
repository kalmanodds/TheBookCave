using System.Collections.Generic;
using System.Linq;
using TheBookCave.Data;
using TheBookCave.Data.EntityModels;
using TheBookCave.Models;
using TheBookCave.Models.InputModels;
using TheBookCave.Models.ViewModels;

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
                IsCurrentOrder = true,
            };
            _db.Add(newOrder);
            _db.SaveChanges();
        }

        public bool UserHasCurrentOrder(string userID)
        {
            var order = (from o in _db.Orders
                       where o.UserID == userID && o.IsCurrentOrder == true
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
                         where o.UserID == userID && o.IsCurrentOrder == true
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
            order.IsCurrentOrder = true;

            _db.Orders.Update(order);
            _db.SaveChanges();
        }

        public void AddOrderFinalized(CheckoutInputModel model, string userID)
        {
            var userOrder = (from o in _db.Orders
                             where o.UserID == userID && o.IsCurrentOrder == true
                             select o).FirstOrDefault();
            var shippingAddress = new AddressModel()
            {
                StreetName = model.StreetName,
                HouseNumber = (int)model.HouseNumber,
                City = model.City,
                Zip = model.Zip,
                Country = model.Country,
            };

            var paymentInfo = new PaymentInfoModel()
            {
                CardType = model.CardType,
                CardHolder = model.CardHolder,
                CardNumber = model.CardNumber,
                Month = model.Month,
                Year = model.Year,
                CVC = model.CVC,
            };

            userOrder.ShippingAddress = shippingAddress;
            userOrder.PaymentInfo = paymentInfo;
            userOrder.IsWrapped = model.IsWrapped;

            _db.Orders.Update(userOrder);
            _db.SaveChanges();
        }

        public OrderViewModel GetCurrentOrder(string userID)
        {
            var order = (from o in _db.Orders
                         where o.UserID == userID && o.IsCurrentOrder == true
                         select new OrderViewModel()
                         {
                             OrderID = o.ID,
                             TotalPrice = o.TotalPrice,
                             ShippingAddress = o.ShippingAddress,
                             DateOrder = o.DateOrder,
                             PaymentInfo = o.PaymentInfo,
                             IsWrapped = o.IsWrapped,
                         }).FirstOrDefault();
            if(order != null)
            {
                return order;
            }
            return null;
        }

        public void ConfirmOrder(int orderID, bool wrapped)
        {
            var order = (from o in _db.Orders
                         where o.ID == orderID
                         select o).FirstOrDefault();
            if(order != null)
            {
                order.IsCurrentOrder = false;
                order.IsReady = true;
                order.IsWrapped = wrapped;
                if(wrapped)
                {
                    order.TotalPrice += 2;
                }
                _db.Orders.Update(order);
                _db.SaveChanges();
            }
        }

        public List<OrderHistoryViewModel> GetOrderHistory(string userID)
        {
            var orders = (from o in _db.Orders
                          where o.UserID == userID && o.IsCurrentOrder == false
                          select new OrderHistoryViewModel()
                          {
                              OrderID = o.ID,
                              TotalPrice = o.TotalPrice,
                              DateOrder = o.DateOrder,
                              IsWrapped = o.IsWrapped,
                          }).ToList();

            for(int i = 0; i < orders.Count(); i++)
            {
                var books = (from c in _db.OrderBookConnections
                             where c.OrderID == orders[i].OrderID
                             select c).ToList();
                
                int amount = 0;
                for(int j = 0; j < books.Count(); j++)
                {
                    amount += books[j].Amount;
                }

                orders[i].Quantity = amount;
            }

            return orders;
        }

    }
}