﻿using System;
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
        //Private member variable to manipulate the book database.
        private BookService _bookService;

        //Constructor the creates new BookService.
        public HomeController()
        {
            _bookService = new BookService();
        }

        //The /Home/Index. The starting page with 3 lists: Bestsellers, Top 10, and newest.
        public IActionResult Index()
        {
            //Array of List is Kalman's idea. Let's see how it plays out.
            //Status Update: Kalman's idea played out better than expected.

            //Gets the first list of books. Bestsellers.
            var filter1 = new FilterModel(0, System.Double.MaxValue, null, "SellerDown", null, 10);
            var bestsellers = _bookService.GetBooks(filter1);

            //Gets the second list of books. Top Ten.
            var filter2 = new FilterModel(0, System.Double.MaxValue, null, "RatingDown", null, 10);
            var topten = _bookService.GetBooks(filter2);

            //Gets the third and final list. Newest.
            var filter3 = new FilterModel(0, System.Double.MaxValue, null, "DatePublishedDown", null, 10);
            var newest = _bookService.GetBooks(filter3);

            //Creates an array of the 3 lists.
            var listArray = new List<BookViewModel>[] {bestsellers, topten, newest};

            //Returns the view with the 3 lists.
            return View(listArray);
        }

        //The catalogue of all books. filter can be default(all books) or applied(filtered books)
        public IActionResult Catalogue(FilterModel filter)
        {
            //Sets Viewbag.
            ViewBag.Title = "All books";

            //Give the ViewBag.Title the value of the genre.
            if(filter.Genre != null)
            {
                ViewBag.Title = filter.Genre + " books";
            }
            //If the user has filtered the list in any way besides the genre he the viewbag get the value "Filtered book list"
            if(filter.MaxPrice != System.Double.MaxValue || filter.MinPrice != 0 || filter.SearchWord != null)
            {
                ViewBag.Title = "Filtered book list";
            }
            //The viewbag message will change depending on what orderBy value the user chose
            switch(filter.OrderBy)
            {
                case "AlphaUp":
                    ViewBag.Title += " ordered alphabetically";
                    break;
                case "AlphaDown":
                    ViewBag.Title += " ordered reverse alphabetically";
                    break;
                case "PriceUp":
                    ViewBag.Title += " ordered by lowest to highest price";
                    break;
                case "PriceDown":
                    ViewBag.Title += " ordered by highest to lowest price";
                    break;
                case "RatingUp":
                    ViewBag.Title += " ordered by lowest to highest rating";
                    break;
                case "RatingDown":
                    ViewBag.Title += " ordered by highest to lowest rating";
                    break;
                case "SellerUp":
                    ViewBag.Title += " ordered by most to least sold";
                    break;
                case "SellerDown":
                    ViewBag.Title += " ordered by least to most sold";
                    break;
                case "DatePublishedUp":
                    ViewBag.Title += " ordered by oldest to newest";
                    break;
                case "DatePublishedDown":
                    ViewBag.Title += " ordered by newest to oldest";
                    break;
                default:
                    ViewBag.Title += " ordered alphabetically";
                    break;
            }

            //Gets all books that have the filter applied.
            var books = _bookService.GetBooks(filter);
            //Returns view with the books the user wanted.
            return View(books);
        }

        //Action that takes in the parameters to created a filtered catalogue.
        public IActionResult FilteredCatalogue(double minPrice = 0, double maxPrice = System.Double.MaxValue, string searchWord = null, string orderBy = null, string genre = null)
        {
            //Creates new filter with the parameters.
            var filter = new FilterModel(minPrice, maxPrice, searchWord, orderBy, genre , 0);
            //Redirects to the Catalogue.
            return RedirectToAction("Catalogue", "Home", filter);
        }

        //The Top 10 rated books.
        public IActionResult TopTen()
        {
            ViewData["Message"] = "The Top 10";
            
            //Gets the 10 books ordered by rating high to low.
            var filter = new FilterModel(0, System.Double.MaxValue, null, "RatingDown", null, 10);
            var books = _bookService.GetBooks(filter);

            //Returns the book in the top ten.
            return View(books);
        }

        //The terms of service for our company.
        public IActionResult TermsOfService()
        {
            return View("TermsOfService");
        }

        //Error Window made by Microsoft.
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //Returns the catalogue filtered from highest selling to lowest.
        public IActionResult BestSellers()
        {
            var filter = new FilterModel(0, System.Double.MaxValue, null, "SellerDown", null, 0);
            return RedirectToAction("Catalogue", "Home", filter);
        }

        //Returns the catalogue filtered from most recent to least recent.
        public IActionResult Newest()
        {
            var filter = new FilterModel(0, System.Double.MaxValue, null, "DatePublishedDown", null, 0);
            return RedirectToAction("Catalogue", "Home", filter);
        }

        //Returns the book of the day.
        public IActionResult BookOfTheDay()
        {
            var book = _bookService.GetBookOfDay();
            return View(book);
        }
    }
}
