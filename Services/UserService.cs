using TheBookCave.Models.InputModels;
using TheBookCave.Models.ViewModels;
using TheBookCave.Repositories;

namespace TheBookCave.Services
{
    public class UserService
    {
        private UserRepo _userRepo;

        public UserService()
        {
            _userRepo = new UserRepo();
        }

        public void AddUser(RegisterInputModel model, string id)
        {
            _userRepo.AddUser(model, id);
        }

        public UserViewModel GetUser(string id)
        {
            var user = _userRepo.GetUser(id);
            return user;
        }

        public void EditUser(UserInputModel model)
        {
            _userRepo.EditUser(model);
        }

        public void ChangeAvatar(string userID, string image)
        {
            _userRepo.ChangeAvatar(userID, image);
        }
    }
}