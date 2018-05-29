using System.Collections.Generic;
using TheBookCave.Models.ViewModels;

namespace TheBookCave.Models.InputModels
{
    public class OrderInputModel
    {
        public string UserID { get; set; }
        public List<BookViewModel> Books { get; set; }
    }
}