using Microsoft.AspNetCore.Mvc;
using TheBookCave.Services;

namespace TheBookCave.Controllers
{
    public class OrderController : Controller
    {
        private OrderService _orderService;

        public IActionResult Index()
        {
            return RedirectToAction("Catalogue", "Home");
        }

        public IActionResult Cart()
        {
            return View();
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