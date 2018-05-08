using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TheBookCave.Models;
using TheBookCave.Models.InputModels;
using TheBookCave.Models.ViewModels;
using TheBookCave.Services;

namespace TheBookCave.Controllers
{
    public class AccountController : Controller
    {
        //Private Member Variables that are used in ASP.NET Identity.
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        //Service Classes that Account Controller influences.
        private BookService _bookService;
        private UserService _userService;
        private CartService _cartService;
        private WishlistService _wishlistService;

        //Constructor that initializes private variables.
        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _bookService = new BookService();
            _userService = new UserService();
            _cartService = new CartService();
            _wishlistService = new WishlistService();
        }

        //Returns the Form to the user with which they will register with.
        public IActionResult Register()
        {
            return View();
        }

        //Function that takes in a registration form and adds the user.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterInputModel model)
        {
            //Returns the form again if the form is invalid.
            if(!ModelState.IsValid)
            {
                return View();
            }

            //Creates new user with the InputModel.
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email};
            var result = await _userManager.CreateAsync(user, model.Password);

            if(result.Succeeded)
            {
                //The user is successfully registered.
                //Add the concatenated first and last name as fullName in claims.
                await _userManager.AddClaimAsync(user, new Claim("Name", $"{model.FirstName} {model.LastName}"));
                await _signInManager.SignInAsync(user, false);

                //Adds User to the DataContext database which can be accessed through UserService.
                var id = user.Id;
                _userService.AddUser(model, id);

                //Redirects to the Index after user has registered.
                return RedirectToAction("Index", "Home");
            }

            //If the Result had not succeded, the user will be presented with the form again.
            return View();
        }

        //Returns the Login form that users use to log in.
        public IActionResult Login()
        {
            return View();
        }

        //Takes in InputModel and Logs in user.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(LoginInputModel model)
        {
            //If the Model is invalid, the user is presented with the form again.
            if(!ModelState.IsValid)
            {
                return View();
            }

            //Fetches user from signInManager.
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            
            if(result.Succeeded)
            {
                //If the user successfully logged in, the user is redirected to Home/Index.
                return RedirectToAction("Index", "Home");
            }

            //If user did not successfully log in, they are presented with the form again.
            return View();
        }

        //Logs user out.
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        //A function that is used to display the Access Denied View.
        public IActionResult AccessDenied()
        {
            return View();
        }

        //Everything above this line is from the Authorization lectures.
        //Everything below this line will be the other methods.

        //The User page. Displays information and functions.
        [Authorize]
        public async Task<IActionResult> Index()
        {
            //Gets user and their id.
            var user = await GetCurrentUserAsync();
            var id = user?.Id;

            if(id == null)
            {
                return View("NotFound");
            }

            //Gets the UserViewModel from UserService using the afforementioned id.
            var displayedUser = _userService.GetUser(id);

            //returns with UserViewModel as @model.
            return View(displayedUser);
        }

        //The Cart page for the current User.
        [Authorize]
        public async Task<IActionResult> Cart()
        {
            //Gets user and their id.
            var user = await GetCurrentUserAsync();
            var id = user?.Id;

            //Receives the Books connected to said user.
            var books = _bookService.GetCartBooks(id);

            //Returns the Cart with all the books connected to the User.
            return View(books);
        }

        //The Wishlist of the User.
        [Authorize]
        public async Task<IActionResult> Wishlist()
        {
            //Gets the user and their id.
            var user = await GetCurrentUserAsync();
            var id = user?.Id;

            //Books that are in said user's wihslist.
            var books = _bookService.GetWishlistBooks(id);

            //Returns the List of Books on the Wishlist.
            return View(books);
        }

        //Helper function that returns current user.
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        //Form in which to edit the user profile.
        [Authorize]
        public async Task<IActionResult> EditProfile()
        {
            //Gets the user and the id.
            var user = await GetCurrentUserAsync();
            var id = user?.Id;

            //Returns the UserViewModel for the current user.
            var userViewModel = _userService.GetUser(id);

            //Returns current values as well as a form to change things.
            return View(userViewModel);
        }

        //Takes in all the Inputs from the form.
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditProfile(string id, string firstName, string lastName, string streetName, int houseNumber, string city, int zip, string country)
        {
            //Creates new AddressModel of the user inputs.
            var address = new AddressModel()
            {
                StreetName = streetName,
                HouseNumber = houseNumber,
                City = city,
                Zip = zip,
                Country = country
            };

            //Creates the UserInputModel which can be partly or fully filled in.
            var newUser = new UserInputModel()
            {
                UserID = id,
                FirstName = firstName,
                LastName = lastName,
                Address = address,
                Image = null,
            };

            var user = await GetCurrentUserAsync();

            //Updates the current user file with the new info.
            _userService.EditUser(newUser);
            if(newUser.FirstName == null)
            {
                newUser.FirstName = _userService.GetUser(user.Id).FirstName;
            }
            if(newUser.LastName == null)
            {
                newUser.LastName = _userService.GetUser(user.Id).LastName;
            }
            
            var allClaims = await _userManager.GetClaimsAsync(user);
            var userClaims = (from c in allClaims
                         where c.Type == "Name"
                         select c).ToList();
            
            await _userManager.RemoveClaimsAsync(user, userClaims);
            await _userManager.AddClaimAsync(user, new Claim("Name", $"{newUser.FirstName} {newUser.LastName}"));

            //Returns the user to their user homepage after the changes.
            return RedirectToAction("Index", "Account");
        }

        //Function that adds books to Cart.
        [Authorize]
        public async Task<IActionResult> AddToCart(int? id)
        {
            if(id == null)
            {
                return View("NotFound");
            }

            //Gets the user and their id.
            var user = await GetCurrentUserAsync();
            var userID = user?.Id;

            //Creates the InputModel for the Cart.
            var newCartItem = new CartInputModel((int)id, userID);
            //Adds this the cart connection from user to book.
            _cartService.AddCartItem(newCartItem);
            //Returns the user to the Catalogue.
            return RedirectToAction("Catalogue", "Home");
        }

        //Function that adds books to the Wishlist.
        [Authorize]
        public async Task<IActionResult> AddToWishlist(int? id)
        {
            if(id == null)
            {
                return View("NotFound");
            }

            //Gets the user and their id
            var user = await GetCurrentUserAsync();
            var userID = user?.Id;

            //Creates the InputModel for the Wishlist.
            var newWishlistItem = new WishlistInputModel((int)id, userID);
            //Adds the wishlist connection from user to book.
            _wishlistService.AddWishlistItem(newWishlistItem);
            //Redirects the user to the Catalogue.
            return RedirectToAction("Catalogue", "Home");
        }
    }
}