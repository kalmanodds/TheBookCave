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

    //Möguleg föll sem við munum útfæra:
        //getUsers()
        //getUser()
        //AddUser()
        //UpdateUser()
        //RemoveUser()
    }
}