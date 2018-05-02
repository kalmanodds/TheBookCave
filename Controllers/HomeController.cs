using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheBookCave.Models;

namespace TheBookCave.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Catalogue()
        {
            ViewData["Message"] = "The Catalogue";

            return View();
        }

        public IActionResult TopTen()
        {
            ViewData["Message"] = "The Top Ten";

            return View();
        }

        public IActionResult TermsOfService()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
