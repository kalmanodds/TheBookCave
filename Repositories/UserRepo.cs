using System.Linq;
using TheBookCave.Data;
using TheBookCave.Data.EntityModels;
using TheBookCave.Models;
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
                Image = "~/images/avatars/robot-03@512px.png",
                FavoriteBookID = null,
                IsPremium = false
            };

            _db.Add(newUser);
            _db.SaveChanges();
        }

        public UserViewModel GetUser(string id)
        {
            var user = (from u in _db.Users
                        where u.UserID == id
                        select new UserViewModel(){
                            UserID = u.UserID,
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

        public void EditUser(UserInputModel model)
        {
            var entityModel = (from u in _db.Users
                               where u.UserID.Equals(model.UserID)
                               select u).SingleOrDefault();

            if(model.FirstName != null)
            {
                entityModel.FirstName = model.FirstName;
            }

            if(model.LastName != null)
            {
                entityModel.LastName = model.LastName;
            }

            var address = new AddressModel();

            if(model.Address.StreetName != null)
            {
                address.StreetName = model.Address.StreetName;
            }

            if(model.Address.HouseNumber != 0)
            {
                address.HouseNumber = model.Address.HouseNumber;
            }

            if(model.Address.City != null)
            {
                address.City = model.Address.City;
            }

            if(model.Address.Zip != 0)
            {
                address.Zip = model.Address.Zip;
            }

            if(model.Address.Country != null)
            {
                address.Country = model.Address.Country;
            }

            if(model.Image != null)
            {
                entityModel.Image = model.Image;
            }

            entityModel.Address = address;

            _db.Users.Update(entityModel);
            _db.SaveChanges();
        }

        public void ChangeAvatar(string userID, string image)
        {
            var user = (from u in _db.Users
                        where u.UserID == userID
                        select u).FirstOrDefault();
            user.Image = image;
            _db.Users.Update(user);
            _db.SaveChanges();
        }
    }
}