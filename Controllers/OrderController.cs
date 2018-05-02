using Microsoft.AspNetCore.Mvc;

namespace TheBookCave.Controllers
{
    public class OrderController : Controller
    {
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