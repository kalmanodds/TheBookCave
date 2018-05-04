using TheBookCave.Data;
using TheBookCave.Data.EntityModels;
using TheBookCave.Models.InputModels;

namespace TheBookCave.Repositories
{
    public class UserRepo
    {
        private DataContext _db;

        public UserRepo()
        {
            _db = new DataContext();
        }

        public void AddUser(RegisterInputModel model)
        {
            var newUser = new UserEntityModel()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Address = null,
                Image = null,
                FavoriteBookID = null,
                IsPremium = false
            };

            _db.Add(newUser);
            _db.SaveChanges();
        }
    }
}