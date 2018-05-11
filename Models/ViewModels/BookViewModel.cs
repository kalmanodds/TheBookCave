using TheBookCave.Models;

namespace TheBookCave.Models.ViewModels
{
    public class BookViewModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN10 { get; set; }
        public string ISBN13 { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Genre { get; set; }
        public int NumberOfPages { get; set; }
        public int NumberOfCopiesSold { get; set; } 
        public DateModel DatePublished { get; set; }
        public string Publisher { get; set; }
        public double Rating { get; set; }
        public string Image { get; set; }
        public int Amount { get; set; }
    }
}
