using Microsoft.AspNetCore.Mvc;
using TheBookCave.Models.InputModels;
using TheBookCave.Models.ViewModels;
using TheBookCave.Services;

namespace TheBookCave.Controllers
{
    public class OrderController : Controller
    {
        private OrderService _orderService;
        private OrderBookConnectionService _obcService;

        public OrderController()
        {
            _orderService = new OrderService();
            _obcService = new OrderBookConnectionService();
        }

        public IActionResult Index()
        {
            return RedirectToAction("Catalogue", "Home");
        }

        public IActionResult CheckOut()
        {
            return View();
        }

        public IActionResult VerifyOrder()
        {
            return View();
        }

        public IActionResult Review(CheckoutInputModel model)
        {
            _orderService.AddOrderFinalized(model, model.UserID);
            var order = _orderService.GetCurrentOrder(model.UserID);
            var listOrderBooks = _obcService.GetBooks(order.OrderID);
            order.Books = listOrderBooks;
            return View(order);
        }
    }
}