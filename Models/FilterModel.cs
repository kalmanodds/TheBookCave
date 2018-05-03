namespace TheBookCave.Models
{
    public class FilterModel
    {
        public double MinPrice { get; set; }
        public double MaxPrice { get; set; }
        public string SearchWord { get; set; }
        public string OrderBy { get; set; }
        //OrderBy options are:
        //AlphaUp
        //AlphaDown
        //PriceUp
        //PriceDown
        //RatingUp
        //RatingDown
        public string Genre { get; set; }
        //Romance
        // TODO add more plz

        public FilterModel(double minPrice = 0, double Maxprice = System.Double.MaxValue, string searchWord = null, string orderBy = "AlphaUp", string genre = null)
        {
            MinPrice = minPrice;
            MaxPrice = MaxPrice;
            SearchWord = searchWord;
            OrderBy = orderBy;
            Genre = genre;
        }
    }
}