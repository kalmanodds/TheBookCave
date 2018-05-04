﻿using System;
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
            //This is Kalman's idea. Let's see how it plays out.
            var filter1 = new FilterModel(0, System.Double.MaxValue, null, "SellerDown", null, 10);
            var bestsellers = _bookService.GetBooks(filter1);

            var filter2 = new FilterModel();
            var topten = _bookService.GetBooks(filter2);

            var filter3 = new FilterModel();
            var newest = _bookService.GetBooks(filter3);



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
