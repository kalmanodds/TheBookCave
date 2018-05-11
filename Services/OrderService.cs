using System;
using System.Collections.Generic;
using TheBookCave.Models;
using TheBookCave.Models.InputModels;
using TheBookCave.Models.ViewModels;
using TheBookCave.Repositories;

namespace TheBookCave.Services
{
    public class OrderService
    {
        private OrderRepo _orderRepo;

        public OrderService()
        {
            _orderRepo = new OrderRepo();
        }

    //Möguleg föll sem við munum útfæra:
        //AddOrder()
        //EditOrder()
        //GetOrders()
        //GetOrder()
        //RemoveOrder()

        public void AddOrder(OrderInputModel model)
        {
            DateTime today = DateTime.Today;
            DateModel dateOrdered = new DateModel()
            {
                Year = today.Year,
                Month = today.Month,
                Day = today.Day
            };
            double totalPrice = 0;
            for(int i = 0; i < model.Books.Count; i++)
            {
                totalPrice += (model.Books[i].Price * model.Books[i].Amount);
            }
            _orderRepo.AddOrder(model, totalPrice, dateOrdered);
        }

        public bool UserHasCurrentOrder(string userID)
        {
            return _orderRepo.UserHasCurrentOrder(userID);
        }

        public int? GetCurrentOrderID(string userID)
        {
            return _orderRepo.GetCurrentOrderID(userID);
        }

        public void UpdateOrder(OrderInputModel model, int orderID)
        {
            DateTime today = DateTime.Today;
            DateModel dateOrdered = new DateModel()
            {
                Year = today.Year,
                Month = today.Month,
                Day = today.Day
            };
            double totalPrice = 0;
            for(int i = 0; i < model.Books.Count; i++)
            {
                totalPrice += (model.Books[i].Price * model.Books[i].Amount);
            }
            _orderRepo.UpdateOrder(model, totalPrice, dateOrdered, orderID);
        }

        public void AddOrderFinalized(CheckoutInputModel model, string userID)
        {
            _orderRepo.AddOrderFinalized(model, userID);
        }

        public OrderViewModel GetCurrentOrder(string userID)
        {
            return _orderRepo.GetCurrentOrder(userID);
        }

        public void ConfirmOrder(int orderID, bool wrapped)
        {
            _orderRepo.ConfirmOrder(orderID, wrapped);
        }

        public List<OrderViewModel> GetOrderHistory(string userID)
        {
            return _orderRepo.GetOrderHistory(userID);
        }
    }
}