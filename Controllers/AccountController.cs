using Microsoft.AspNetCore.Mvc;

namespace TheBookCave.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Account()
        {
            return View();
        }

        public IActionResult OrderHistory()
        {
            return View();
        }

        public IActionResult EditProfile()
        {
            return View();
        }

        public IActionResult SignIn()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
    }
}