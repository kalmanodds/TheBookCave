using System.Linq;
using TheBookCave.Data;
using TheBookCave.Data.EntityModels;
using TheBookCave.Models.InputModels;
using TheBookCave.Models.ViewModels;

namespace TheBookCave.Repositories
{
    public class UserRepo
    {
        private DataContext _db;

        public UserRepo()
        {
            _db = new DataContext();
        }

        public void AddUser(RegisterInputModel model, string id)
        {
            var newUser = new UserEntityModel()
            {
                UserID = id,
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

        public UserViewModel GetUser(string id)
        {
            var user = (from u in _db.Users
                        where u.UserID.Equals(id)
                        select new UserViewModel(){
                            FirstName = u.FirstName,
                            LastName = u.LastName,
                            Email = u.Email,
                            Address = u.Address,
                            Image = u.Image,
                            FavoriteBookID = u.FavoriteBookID,
                            IsPremium = u.IsPremium
                        }).FirstOrDefault();
            return user;
        }
    }
}