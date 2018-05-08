using Microsoft.AspNetCore.Mvc;
using TheBookCave.Services;

namespace TheBookCave.Controllers
{
    public class OrderController : Controller
    {
        private OrderService _orderService;

        public OrderController()
        {
            _orderService = new OrderService();
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
    }
}