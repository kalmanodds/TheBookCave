using TheBookCave.Models.InputModels;
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

        public void AddUser(RegisterInputModel model)
        {
            _userRepo.AddUser(model);
        }
    }
}