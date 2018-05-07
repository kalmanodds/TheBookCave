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
        //SellerUp
        //SellerDown
        //DatePublishedUp
        //DatePublishedDown
        public string Genre { get; set; }
        //Romance
        // TODO add more plz
        public int Amount { get; set; }

        public FilterModel()
        {
            MinPrice = 0;
            MaxPrice = System.Double.MaxValue;
            SearchWord = null;
            OrderBy = null;
            Genre = null;
            Amount = 0;
        }

        public FilterModel(double minPrice = 0, double maxPrice = System.Double.MaxValue, string searchWord = null, string orderBy = "AlphaUp", string genre = null, int amount = 0)
        {
            MinPrice = minPrice;
            MaxPrice = maxPrice;
            SearchWord = searchWord;
            OrderBy = orderBy;
            Genre = genre;
            Amount = amount;
        }
    }
}