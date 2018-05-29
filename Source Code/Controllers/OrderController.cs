using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheBookCave.Models.InputModels;
using TheBookCave.Models.ViewModels;
using TheBookCave.Services;

namespace TheBookCave.Controllers
{
    public class OrderController : Controller
    {
        //Private member variables so that orders and order book connections are manipulatable.
        private OrderService _orderService;
        private OrderBookConnectionService _obcService;

        //Constructor to initialize private variables.
        public OrderController()
        {
            _orderService = new OrderService();
            _obcService = new OrderBookConnectionService();
        }

        //Order Index redirects to the Starting page.
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        //Returns the view for checkout.
        public IActionResult CheckOut()
        {
            return View();
        }

        //The verify order view where user adds to the order.
        public IActionResult VerifyOrder()
        {
            return View();
        }

        //The review view where user can confirm their order.
        public IActionResult Review(CheckoutInputModel model)
        {
            //Finalizes order.
            _orderService.AddOrderFinalized(model, model.UserID);
            //Gets current order.
            var order = _orderService.GetCurrentOrder(model.UserID);
            //Gets the books for the current order.
            var listOrderBooks = _obcService.GetBooks(order.OrderID);
            //Adds the books to the model.
            order.Books = listOrderBooks;
            //And returns the model so user can go over the books in the review phase.
            return View(order);
        }

        //The Function that sets order to shipped.
        [HttpPost]
        [Authorize(Roles="staff")]
        public IActionResult ShipOrder(int? orderID)
        {
            if(orderID != null)
            {
                _orderService.ShipOrder((int)orderID);
            }

            return RedirectToAction("Orders", "Account");
        }
    }
}