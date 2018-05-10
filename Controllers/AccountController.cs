using System;
using System.Collections.Generic;
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
        private OrderService _orderService;
        private OrderBookConnectionService _obcService;
        private RatingService _ratingService;

        //Constructor that initializes private variables.
        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _bookService = new BookService();
            _userService = new UserService();
            _cartService = new CartService();
            _wishlistService = new WishlistService();
            _orderService = new OrderService();
            _obcService = new OrderBookConnectionService();
            _ratingService = new RatingService();
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
            ViewData["ErrorMessage"] = "";

            //Fetches user from signInManager.
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            
            if(result.Succeeded)
            {
                //If the user successfully logged in, the user is redirected to Home/Index.
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["ErrorMessage"] = "Make sure you enter the right email and password";
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
            var user = await _userManager.GetUserAsync(User);
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
            var user = await _userManager.GetUserAsync(User);
            var id = user?.Id;

            //Receives the Books connected to said user.
            var books = _cartService.GetCartBooks(id);

            //Returns the Cart with all the books connected to the User.
            return View(books);
        }

        //The Wishlist of the User.
        [Authorize]
        public async Task<IActionResult> Wishlist()
        {
            //Gets the user and their id.
            var user = await _userManager.GetUserAsync(User);
            var id = user?.Id;

            //Books that are in said user's wihslist.
            var books = _bookService.GetWishlistBooks(id);

            //Returns the List of Books on the Wishlist.
            return View(books);
        }

        //Form in which to edit the user profile.
        [Authorize]
        public async Task<IActionResult> EditProfile()
        {
            //Gets the user and the id.
            var user = await _userManager.GetUserAsync(User);
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

            var user = await _userManager.GetUserAsync(User);

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
        [HttpPost]
        public async Task<IActionResult> AddToCart(int? id)
        {
            if(id == null)
            {
                return View("NotFound");
            }

            //Gets the user and their id.
            var user = await _userManager.GetUserAsync(User);
            var userID = user?.Id;

            //Creates the InputModel for the Cart.
            var newCartItem = new CartInputModel()
            {
                BookID = (int)id,
                UserID = userID,
            };
            //Adds this the cart connection from user to book.
            _cartService.AddCartItem(newCartItem);
            //Returns the user to the Catalogue.
            return RedirectToAction("Catalogue", "Home");
        }

        //Function that adds books to the Wishlist.
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddToWishlist(int? id)
        {
            if(id == null)
            {
                return View("NotFound");
            }

            //Waits half a second so the add to wishlist animation can finish
            System.Threading.Thread.Sleep(500);

            //Gets the user and their id
            var user = await _userManager.GetUserAsync(User);
            var userID = user?.Id;

            //Creates the InputModel for the Wishlist.
            var newWishlistItem = new WishlistInputModel((int)id, userID);
            //Adds the wishlist connection from user to book.
            _wishlistService.AddWishlistItem(newWishlistItem);
            //Redirects the user to the Catalogue.
            return RedirectToAction("Catalogue", "Home");
        }

        [Authorize]
        public IActionResult ChooseAvatar()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChooseAvatar(string image)
        {
            var user = await _userManager.GetUserAsync(User);
            var userID = user.Id;
            _userService.ChangeAvatar(userID, image);
            return RedirectToAction("EditProfile", "Account");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> MakeOrder(string id)
        {
            if(id != "rghgjhvjhvdjhjh45347yhu")
            {
                return RedirectToAction("Index", "Home");
            }

            var user = await _userManager.GetUserAsync(User);
            var userID = user.Id;
            var booksInCart = _cartService.GetCartBooks(userID);

            var newOrder = new OrderInputModel()
            {
                UserID = userID,
                Books = booksInCart,
            };

            bool userHasCurrentOrder = _orderService.UserHasCurrentOrder(userID);

            if(!userHasCurrentOrder)
            {
                _orderService.AddOrder(newOrder);
                var orderID = _orderService.GetCurrentOrderID(userID);
                for(int i = 0; i < booksInCart.Count; i++)
                {
                    _obcService.AddConnection((int)orderID, booksInCart[i].ID, booksInCart[i].Amount);
                }
            }
            else
            {
                var orderID = _orderService.GetCurrentOrderID(userID);
                _orderService.UpdateOrder(newOrder, (int)orderID);
                for(int i = 0; i < booksInCart.Count; i++)
                {
                    _obcService.UpdateConnection((int)orderID, booksInCart[i].ID, booksInCart[i].Amount);
                }
            }

            return RedirectToAction("CheckOut", "Order");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> OrderCheckOutToVerify(CheckoutInputModel model)
        {
            if(!ModelState.IsValid)
            {
                return RedirectToAction("CheckOut", "Order");
            }
            var user = await _userManager.GetUserAsync(User);
            var userID = user.Id;
            _orderService.AddOrderFinalized(model, userID);
            return RedirectToAction("Review", "Order");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ConfirmedOrder(OrderViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateCart(int bookID, int amount)
        {
            //var user = await _userManager.GetUserAsync(User);
            //var userID = user.Id;
            var user = await _userManager.GetUserAsync(User);
            var userID = user.Id;
            var model = new CartInputModel()
            {
                UserID = userID,
                BookID = bookID,
                Amount = amount,
            };
            _cartService.UpdateConnection(model);
            return RedirectToAction("Cart", "Account");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> RemoveCart(int bookID)
        {
            var user = await _userManager.GetUserAsync(User);
            var userID = user.Id;
            _cartService.RemoveItem(userID, bookID);

            bool userHasCurrentOrder = _orderService.UserHasCurrentOrder(userID);

            if(userHasCurrentOrder)
            {
                var orderID = _orderService.GetCurrentOrderID(userID);
                _obcService.RemoveItem((int)orderID, bookID);
            }

            return RedirectToAction("Cart", "Account");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> RemoveWishlist(int bookID)
        {
            var user = await _userManager.GetUserAsync(User);
            var userID = user.Id;

            _wishlistService.RemoveItem(userID, bookID);

            return RedirectToAction("Wishlist", "Account");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> MoveWishlistToCart(int bookID)
        {
            var user = await _userManager.GetUserAsync(User);
            var userID = user.Id;

            _wishlistService.RemoveItem(userID, bookID);
            var cartItem = new CartInputModel()
            {
                BookID = bookID,
                UserID = userID,
            };
            _cartService.AddCartItem(cartItem);

            return RedirectToAction("Wishlist", "Account");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddRating(int score, string comment, int bookID)
        {
            var user = await _userManager.GetUserAsync(User);
            var userID = user.Id;
            
            var newReview = new RatingInputModel()
            {
                Score = score,
                Comment = comment,
                BookID = bookID,
                UserID = userID,
            };

            _ratingService.AddRating(newReview);
            _bookService.AddRating(newReview);

            return RedirectToAction("Details", "Book", new {id = bookID});
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> MakeFavorite(int bookID)
        {
            var user = await _userManager.GetUserAsync(User);
            var userID = user.Id;

            _userService.MakeFavorite(userID, bookID);

            return RedirectToAction("Details", "Book", new {id = bookID});
        }

        public IActionResult Yeet()
        {
            return View();
        }
    }
}