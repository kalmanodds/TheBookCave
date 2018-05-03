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
        
    //Möguleg föll sem við munum útfæra:
        //getBooks()
        //getBook()
        //AddBook()
        //UpdateBook()
        //RemoveBook()
    }
}