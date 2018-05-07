using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheBookCave.Models;
using TheBookCave.Models.ViewModels;
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
            //This is Kalman's idea. Let's see how it plays out.
            //Status Update: Kalman's idea played out better than expected.
            var filter1 = new FilterModel(0, System.Double.MaxValue, null, "SellerDown", null, 10);
            var bestsellers = _bookService.GetBooks(filter1);

            var filter2 = new FilterModel(0, System.Double.MaxValue, null, "RatingDown", null, 10);
            var topten = _bookService.GetBooks(filter2);

            var filter3 = new FilterModel(0, System.Double.MaxValue, null, "DatePublishedDown", null, 10);
            var newest = _bookService.GetBooks(filter3);

            var listArray = new List<BookViewModel>[] {bestsellers, topten, newest};

            return View(listArray);
        }

        public IActionResult Catalogue(FilterModel filter)
        {
            if(filter == null)
            {
                var generalFilter = new FilterModel();
                var allBooks = _bookService.GetBooks(generalFilter);
                return View(allBooks);
            }
            var books = _bookService.GetBooks(filter);
            return View(books);
        }

        public IActionResult FilteredCatalogue(double minPrice = 0, double maxPrice = System.Double.MaxValue, string searchWord = null, string orderBy = null, string genre = null)
        {
            var filter = new FilterModel(minPrice, maxPrice, searchWord, orderBy, genre , 0);
            return RedirectToAction("Catalogue", "Home", filter);
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

        public IActionResult BestSellers()
        {
            var filter = new FilterModel(0, System.Double.MaxValue, null, "SellerDown", null, 0);
            return RedirectToAction("Catalogue", "Home", filter);
        }

        public IActionResult Newest()
        {
            var filter = new FilterModel(0, System.Double.MaxValue, null, "DatePublishedDown", null, 0);
            return RedirectToAction("Catalogue", "Home", filter);
        }
    }
}
