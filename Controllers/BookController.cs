using System;
using Microsoft.AspNetCore.Mvc;
using TheBookCave.Models.ViewModels;
using TheBookCave.Services;

namespace TheBookCave.Controllers
{
    public class BookController : Controller
    {
        //Private member variables of what databases this controller can access.
        private BookService _bookService;
        private RatingService _ratingService;

        //Constructor that initializes Services.
        public BookController(){
            _bookService = new BookService();
            _ratingService = new RatingService();
        }

        // /Book redirects to the Catalogue.
        public IActionResult Index()
        {
            return RedirectToAction("Catalogue", "Home");
        }

        //NokkviKilla made this so this does work.
        //The function receives the details of one book.
        public IActionResult Details(int? id)
        {
            //This function takes in a bookID parameter which is the ID of the book that should have details.
            if(id == null)
            {
                //If a book is not specified, redirect to the not found page.
                return View("NotFound");
            }

            //Returns the BookViewModel of the book.
            var book = _bookService.GetBook((int)id);

            if(book == null){
                return View("NotFound");
            }

            //Returns the Details View for the book.
            return View(book);
        }
        
        //TODO
        //The Rating Page for the Book.
        public IActionResult Rating(int id)
        {
            //This function takes in a bookID parameter which is the ID of the book that should have ratings.
            if(id == 0)
            {
                //If a book is not specified, redirect to the catalogue.
                RedirectToAction("Home/Catalogue");
            }
            
            var book = _bookService.GetBook(id);
            
            return View();
        }

        //TODO
        //Editor for Books.
        //Should only be accessable by Employees.
        public IActionResult Edit(int bookID)
        {
            //This function takes in a bookID parameter which is the ID of the book that should be edited.
            if(bookID == 0)
            {
                //If a book is not specified, redirect to the catalogue.
                RedirectToAction("Home/Catalogue");
            }
            
            var book = _bookService.GetBook(bookID);
            
            return View();
        }
    }
}