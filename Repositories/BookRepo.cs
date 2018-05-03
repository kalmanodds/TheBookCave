using TheBookCave.Data;

namespace TheBookCave.Repositories
{
    public class BookRepo
    {
        private DataContext _db;

        public BookRepo()
        {
            _db = new DataContext();
        }

        //getters and setters and yeeters
    }
}