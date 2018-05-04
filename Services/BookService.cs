using System.Collections.Generic;
using TheBookCave.Models;
using TheBookCave.Models.ViewModels;
using TheBookCave.Repositories;

namespace TheBookCave.Services
{
    public class BookService
    {
        private BookRepo _bookRepo;

        public BookService()
        {
            _bookRepo = new BookRepo();
        }
        
        public BookViewModel GetBook(int id)
        {
            return _bookRepo.GetBook(id);
        }

        public List<BookViewModel> GetBooks(FilterModel filter)
        {
            return _bookRepo.GetBooks(filter);
        }

        public List<BookViewModel> GetCartBooks(string userID)
        {
            return _bookRepo.GetCartBooks(userID);
        }

    //Möguleg föll sem við munum útfæra:
        //AddBook()
        //UpdateBook()
        //RemoveBook()
    }
}