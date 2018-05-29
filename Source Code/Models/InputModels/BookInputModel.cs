using System.ComponentModel.DataAnnotations;

namespace TheBookCave.Models.InputModels
{
    public class BookInputModel
    {
        [Required(ErrorMessage="Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage="Author is required")]
        public string Author { get; set; }
        [Required(ErrorMessage="ISBN10 is required")]
        public string ISBN10 { get; set; }
        [Required(ErrorMessage="ISBN13 is required")]
        public string ISBN13 { get; set; }
        [Required(ErrorMessage="Description is required")]
        public string Description { get; set; }
        [Required(ErrorMessage="Price is required")]
        public double Price { get; set; }
        [Required(ErrorMessage="Genre is required")]
        public string Genre { get; set; }
        [Required(ErrorMessage="Number of pages is required")]
        public int NumberOfPages { get; set; }
        [Required(ErrorMessage="Publishing day is required")]
        public int DayPublished { get; set; }
        [Required(ErrorMessage="Publishing month is required")]
        public int MonthPublished { get; set; }
        [Required(ErrorMessage="Publishing yeas is required")]
        public int YearPublished { get; set; }
        [Required(ErrorMessage="Publisher is required")]
        public string Publisher { get; set; }
        [Required(ErrorMessage="Instock is required")]
        public int InStock { get; set; }
        [Required(ErrorMessage="Image is required")]
        public string Image { get; set; }
    }
}