namespace TheBookCave.Models.InputModels
{
    public class BookInputModel
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN10 { get; set; }
        public string ISBN13 { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Genre { get; set; }
        public int NumberOfPages { get; set; }
        public int DayPublished { get; set; }
        public int MonthPublished { get; set; }
        public int YearPublished { get; set; }
        public string Publisher { get; set; }
        public int InStock { get; set; }
        public string Image { get; set; }
    }
}