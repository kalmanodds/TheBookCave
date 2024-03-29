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
        //Private member variable to manipulate database
        private DataContext _db;

        //Constructor that initialzes Database
        public OrderRepo()
        {
            _db = new DataContext();
        }

        //Creates new order.
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

        //Returns true if user has an order.
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

        //Gets current order for user.
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

        //Updates order.
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

        //Adds shippin info in the additional check out phase.
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

        //Gets Current Order for the user.
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

        //Confirms order and makes it no longer current order.
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

        //Gets order history for the user.
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

        //Gets all ready orders. No longer current and yet to be shipped.
        public List<OrderViewModel> GetOrders()
        {
            var orders = (from o in _db.Orders
                          where o.IsReady == true
                          select new OrderViewModel()
                          {
                              OrderID = o.ID,
                              TotalPrice = o.TotalPrice,
                              ShippingAddress = o.ShippingAddress,
                              DateOrder = o.DateOrder,
                              IsWrapped = o.IsWrapped,
                          }).ToList();
            for(int i = 0; i < orders.Count(); i++)
            {
                var books = (from b in _db.Books
                             join c in _db.OrderBookConnections on b.ID equals c.BookID
                             where c.OrderID == orders[i].OrderID
                             select new BookViewModel()
                             {
                                ID = b.ID,
                                Title = b.Title,
                                Author = b.Author,
                                ISBN10 = b.ISBN10,
                                ISBN13 = b.ISBN13,
                                Description = b.Description,
                                Price = b.Price,
                                Genre = b.Genre,
                                NumberOfPages = b.NumberOfPages,
                                NumberOfCopiesSold = b.NumberOfCopiesSold,
                                DatePublished = b.DatePublished,
                                Publisher = b.Publisher,
                                Rating = b.Rating,
                                Image = b.Image,
                                Amount = c.Amount,
                             }).ToList();
                orders[i].Books = books;
            }
            return orders;
        }

        //Sets order to shipped.
        public void ShipOrder(int orderID)
        {
            var order = (from o in _db.Orders
                         where o.ID == orderID
                         select o).FirstOrDefault();

            order.IsReady = false;
            order.IsShipped = true;

            _db.Orders.Update(order);
            _db.SaveChanges();
        }
    }
}