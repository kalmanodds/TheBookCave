namespace TheBookCave.Models
{
    public struct FilterModel
    {
        public double MinPrice { get; set; }
        public double MaxPrice { get; set; }
        public string SearchWord { get; set; }
        public string OrderBy { get; set; }
        public string Genre { get; set; }
    }
}