namespace TheBookCave.Models
{
    public class PaymentInfoModel
    {
        public int ID { get; set; }
        public string CardType { get; set; }
        public string CardHolder { get; set; }
        public string CardNumber { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        public string CVC { get; set; }
    }
}