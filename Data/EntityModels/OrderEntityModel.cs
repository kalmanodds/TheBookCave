using TheBookCave.Models;

namespace TheBookCave.Data.EntityModels
{
    public class OrderEntityModel
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public double TotalPrice { get; set; }
        public AddressModel ShippingAddress { get; set; }
        public DateModel DateOrder { get; set; }
        public PaymentInfoModel PaymentInfo { get; set; }
        public bool IsCurrentOrder { get; set; }
        public bool IsReady { get; set; }
        public bool IsShipped { get; set; }
        public bool IsReceived { get; set; }
        public bool IsWrapped { get; set; }
    }
}
