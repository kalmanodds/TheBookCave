using System;
using Microsoft.AspNetCore.Mvc;
using TheBookCave.Models.ViewModels;
using TheBookCave.Services;

namespace TheBookCave.Controllers
{
    public class BookController : Controller
    {
        private BookService _bookService;
        private RatingService _ratingService;

        public BookController(){
            _bookService = new BookService();
            _ratingService = new RatingService();
        }

        public IActionResult Index()
        {
            return RedirectToAction("Catalogue", "Home");
        }

        //NokkviKilla made this so this does not work
        public IActionResult Details(int? id)
        {
            //This function takes in a bookID parameter which is the ID of the book that should have details.
            if(id == null)
            {
                //If a book is not specified, redirect to the not found page
                return View("NotFound");
            }
            
            var book = _bookService.GetBook((int)id);

            if(book == null){
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