using System.Collections.Generic;
using TheBookCave.Models;

namespace TheBookCave.Models.ViewModels
{
    public class BookDetailsViewModel
    {
        public BookViewModel Book { get; set; }
        public List<RatingViewModel> Ratings { get; set; }
    }
}
