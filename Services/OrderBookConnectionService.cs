using TheBookCave.Repositories;

namespace TheBookCave.Services
{
    public class OrderBookConnectionService
    {
        private OrderBookConnectionRepo _obcRepo;

        public OrderBookConnectionService()
        {
            _obcRepo = new OrderBookConnectionRepo();
        }

        public void AddConnection(int orderID, int bookID, int amount)
        {
            _obcRepo.AddConnection(orderID, bookID, amount);
        }

        public void UpdateConnection(int orderID, int bookID, int amount)
        {
            _obcRepo.UpdateConnection(orderID, bookID, amount);
        }
    }
}