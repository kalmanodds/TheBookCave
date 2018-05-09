using System.Collections.Generic;
using TheBookCave.Models;

namespace TheBookCave.Models.ViewModels
{
    public class OrderViewModel
    {
        public List<BookViewModel> Books { get; set; }
        public double TotalPrice { get; set; }
        public AddressModel ShippingAddress { get; set; }
        public DateModel DateOrder { get; set; }
        public PaymentInfoModel PaymentInfo { get; set; }
        public bool IsReady { get; set; }
        public bool IsShipped { get; set; }
        public bool IsReceived { get; set; }
    }
}
