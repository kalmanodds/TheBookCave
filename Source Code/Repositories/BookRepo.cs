using System;
using System.Collections.Generic;
using System.Linq;
using TheBookCave.Data;
using TheBookCave.Data.EntityModels;
using TheBookCave.Models;
using TheBookCave.Models.InputModels;
using TheBookCave.Models.ViewModels;

namespace TheBookCave.Repositories
{
    public class BookRepo
    {
        //The private member variable for the database.
        private DataContext _db;

        //Constructs the Database.
        public BookRepo()
        {
            _db = new DataContext();
        }

        //Gets Book from ID.
        public BookViewModel GetBook(int? id)
        {
            //Searches for book where the ID matches.
            var book = (from b in _db.Books
                        where (int)id == b.ID
                        select new BookViewModel{
                            ID = b.ID,
                            Title = b.Title,
                            Author = b.Author,
                            ISBN10 = b.ISBN10,
                            ISBN13 = b.ISBN13,
                            Description = b.Description,
                            Price = b.Price,
                            Genre = b.Genre,
                            NumberOfPages = b.NumberOfPages,
                            NumberOfCopiesSold = b.NumberOfCopiesSold,
                            DatePublished = b.DatePublished,
                            Publisher = b.Publisher,
                            Rating = b.Rating,
                            Image = b.Image
                        }).FirstOrDefault();

            return book;
        }

        //Gets all book that fit the filter.
        public List<BookViewModel> GetBooks(FilterModel filter)
        {
            //Firstly filters by min and max price. The default values of which are 0 and System.Double.MaxValue.
            var books = (from b in _db.Books
                         where b.Price >= filter.MinPrice && b.Price <= filter.MaxPrice
                         select new BookViewModel{
                            ID = b.ID,
                            Title = b.Title,
                            Author = b.Author,
                            ISBN10 = b.ISBN10,
                            ISBN13 = b.ISBN13,
                            Description = b.Description,
                            Price = b.Price,
                            Genre = b.Genre,
                            NumberOfPages = b.NumberOfPages,
                            NumberOfCopiesSold = b.NumberOfCopiesSold,
                            DatePublished = b.DatePublished,
                            Publisher = b.Publisher,
                            Rating = b.Rating,
                            Image = b.Image
                         });

            //If Genre is specified, gets books with said genre.
            if(filter.Genre != null)
            {
                books = books.Where( book => book.Genre.ToLower().Equals(filter.Genre.ToLower()));
            }

            //If Search word is specified, gets books that contain the search word in title, author, and isbn.
            if(filter.SearchWord != null)
            {
                books = books.Where( book => (book.Title.ToLower().Contains(filter.SearchWord.ToLower()) 
                                           || book.Author.ToLower().Contains(filter.SearchWord.ToLower())
                                           || book.ISBN10.ToLower().Contains(filter.SearchWord.ToLower())
                                           || book.ISBN13.ToLower().Contains(filter.SearchWord.ToLower()) ) );
            }

            //Orders by the different predetermined ordering ways.
            switch(filter.OrderBy) {
                case "AlphaUp":
                    books = books.OrderBy(book => book.Title);
                    break;
                case "AlphaDown":
                    books = books.OrderByDescending(book => book.Title);
                    break;
                case "PriceUp":
                    books = books.OrderBy(book => book.Price);
                    break;
                case "PriceDown":
                    books = books.OrderByDescending(book => book.Price);
                    break;
                case "RatingUp":
                    books = books.OrderBy(book => book.Rating);
                    break;
                case "RatingDown":
                    books = books.OrderByDescending(book => book.Rating);
                    break;
                case "SellerUp":
                    books = books.OrderBy(book => book.NumberOfCopiesSold);
                    break;
                case "SellerDown":
                    books = books.OrderByDescending(book => book.NumberOfCopiesSold);
                    break;
                case "DatePublishedUp":
                    books = books.OrderBy(book => book.DatePublished);
                    break;
                case "DatePublishedDown":
                    books = books.OrderByDescending(book => book.DatePublished);
                    break;
                default:
                    books = books.OrderBy(book => book.Title);
                    break;
            }

            //Takes a certain amount if it is specified.
            if(filter.Amount != 0)
            {
                books = books.Take(filter.Amount);
            }

            //Initializes the remaining filtered books.
            var result = new List<BookViewModel>();
            if(books == null || books.Count() == 0)
            {
                return null;
            }
            //Turns result from IQueriable to List<>
            result = books.ToList();
            //Returns the list.
            return result;
        }

        //Returns all books in wishlist for certain user.
        public List<BookViewModel> GetWishlistBooks(string userID)
        {
            //Selects books where user id's match in the connection class.
            var books = (from b in _db.Books
                         join c in _db.UserBookWishlistConnections on b.ID equals c.BookID
                         where c.UserID == userID
                         select new BookViewModel()
                         {
                            ID = b.ID,
                            Title = b.Title,
                            Author = b.Author,
                            Description = b.Description,
                            Price = b.Price,
                            NumberOfPages = b.NumberOfPages,
                            NumberOfCopiesSold = b.NumberOfCopiesSold,
                            DatePublished = b.DatePublished,
                            Publisher = b.Publisher,
                            Rating = b.Rating,
                            Image = b.Image,
                         }
                        ).ToList();
            return books;
        }

        //Updates rating value for the book.
        public void AddRating(RatingInputModel model)
        {
            //Finds the book.
            var book = (from b in _db.Books
                        where b.ID == model.BookID
                        select b).First();

            if(book != null)
            {
                //Creates a new rating based on the average of all ratings.
                double newRating = (((book.Rating * book.NumberOfRatings) + model.Score) / (book.NumberOfRatings + 1));
                book.Rating = newRating;
                book.NumberOfRatings++;
                //Updates the book.
                _db.Books.Update(book);
                _db.SaveChanges();
            }
        }

        //Adds Book to the Database.
        public void AddBook(BookInputModel model)
        {
            var book = new BookEntityModel()
            {
                Title = model.Title,
                Author = model.Author,
                ISBN10 = model.ISBN10,
                ISBN13 = model.ISBN13,
                Description = model.Description,
                Price = model.Price,
                Genre = model.Genre,
                NumberOfPages = model.NumberOfPages,
                NumberOfCopiesSold = 0,
                Publisher = model.Publisher,
                Rating = 0,
                NumberOfRatings = 0,
                InStock = model.InStock,
                Image = model.Image,
            };

            var datePublished = new DateModel()
            {
                Day = model.DayPublished,
                Month = model.MonthPublished,
                Year = model.YearPublished,
            };
            book.DatePublished = datePublished;
            _db.Books.Add(book);
            _db.SaveChanges();
        }

        //Gets book of the day.
        public BookViewModel GetBookOfDay()
        {
            //Creates a random index with todays day as the seed.
            int rand = new Random(new DateTime().Day).Next();
            var allBooks = (from b in _db.Books
                            select new BookViewModel()
                            {
                            ID = b.ID,
                            Title = b.Title,
                            Author = b.Author,
                            ISBN10 = b.ISBN10,
                            ISBN13 = b.ISBN13,
                            Description = b.Description,
                            Price = b.Price,
                            Genre = b.Genre,
                            NumberOfPages = b.NumberOfPages,
                            NumberOfCopiesSold = b.NumberOfCopiesSold,
                            DatePublished = b.DatePublished,
                            Publisher = b.Publisher,
                            Rating = b.Rating,
                            Image = b.Image
                            }).ToList();

            //Modulates it to size of all books.
            int index = rand % allBooks.Count();
            //Selects Book.
            var book = allBooks[index];
            //And returns it.
            return book;
        }
    }
}