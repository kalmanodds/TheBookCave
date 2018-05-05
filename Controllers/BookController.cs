using System;
using Microsoft.AspNetCore.Mvc;
using TheBookCave.Services;

namespace TheBookCave.Controllers
{
    public class BookController : Controller
    {
        private BookService _bookService;
        private RatingService _ratingService;

        public IActionResult Index()
        {
            return RedirectToAction("Catalogue", "Home");
        }

        //NokkviKilla made this so this does not work
        public IActionResult Details(int? bookID)
        {
            //This function takes in a bookID parameter which is the ID of the book that should have details.
            if(bookID == null)
            {
                //If a book is not specified, redirect to the not found page
                Console.WriteLine("HALLO HERNA ER EG******************************");
                return View("NotFound");
            }
            var book = _bookService.GetBook(bookID);

            if(book == null){
                Console.WriteLine("*********************HELLO HERE I AM");
                return View("NotFound");
            }

            //should return View with book as parameter
            return View(book);
        }

        public IActionResult Rating(int bookID)
        {
            //This function takes in a bookID parameter which is the ID of the book that should have ratings.
            if(bookID == 0)
            {
                //If a book is not specified, redirect to the catalogue
                RedirectToAction("Home/Catalogue");
            }
            //var book = getBook(bookID);
            //should return View with book as parameter
            return View();
        }

        public IActionResult Edit(int bookID)
        {
            //This function takes in a bookID parameter which is the ID of the book that should be edited.
            if(bookID == 0)
            {
                //If a book is not specified, redirect to the catalogue
                RedirectToAction("Home/Catalogue");
            }
            //var book = getBook(bookID);
            //should return View with book as parameter
            return View();
        }
    }
}