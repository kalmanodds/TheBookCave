using TheBookCave.Repositories;

namespace TheBookCave.Services
{
    public class OrderService
    {
        private OrderRepo _orderRepo;

        public OrderService()
        {
            _orderRepo = new OrderRepo();
        }

    //Möguleg föll sem við munum útfæra:
        //AddOrder()
        //EditOrder()
        //GetOrders()
        //GetOrder()
        //RemoveOrder()
    }
}