using TheBookCave.Models;

namespace TheBookCave.Data.EntityModels.OrderEntityModel
{
    public class OrderEntityModel
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public double TotalPrice { get; set; }
        public AddressModel ShippingAddress { get; set; }
        public DateModel DateOrder { get; set; }
        public bool IsReady { get; set; }
        public bool IsShipped { get; set; }
        public bool IsReceived { get; set; }
    }
}
