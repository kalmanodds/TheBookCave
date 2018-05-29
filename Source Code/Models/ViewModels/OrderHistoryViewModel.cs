namespace TheBookCave.Models.ViewModels
{
    public class OrderHistoryViewModel
    {
        public int OrderID { get; set; }
        public DateModel DateOrder { get; set; }
        public int Quantity { get; set; }
        public bool IsWrapped { get; set; }
        public double TotalPrice { get; set; }
    }
}