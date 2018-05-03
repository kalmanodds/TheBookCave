using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheBookCave.Models;
using TheBookCave.Services;

namespace TheBookCave.Controllers
{
    public class HomeController : Controller
    {
        private BookService _bookService;

        public HomeController()
        {
            _bookService = new BookService();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Catalogue()
        {
            ViewData["Message"] = "The Catalogue";

            var filter = new FilterModel();
            var books = _bookService.GetBooks(filter);

            return View(books);
        }

        public IActionResult TopTen()
        {
            ViewData["Message"] = "The Top Ten";

            var filter = new FilterModel(0, System.Double.MaxValue, null, "RatingDown", null, 10);
            var books = _bookService.GetBooks(filter);

            return View(books);
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
