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
        private readonly RoleManager<IdentityRole> _roleManager;

        //Service Classes that Account Controller influences.
        private BookService _bookService;
        private UserService _userService;
        private CartService _cartService;
        private WishlistService _wishlistService;
        private OrderService _orderService;
        private OrderBookConnectionService _obcService;
        private RatingService _ratingService;

        //Constructor that initializes private variables.
        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
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

            //Creates staff role.
            IdentityResult roleResult;
            var roleExist = await _roleManager.RoleExistsAsync("staff");
            if(!roleExist)
            {
                roleResult = await _roleManager.CreateAsync(new IdentityRole("staff"));
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

                //If new user is to be employee, uncomment next line.
                //await _userManager.AddToRoleAsync(user, "staff");

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

        //Everything above this line is mostly from the Authorization lectures.
        //Everything below this line will be the other methods.

        //The User page. Displays information and functions.
        [Authorize]
        public async Task<IActionResult> Index()
        {
            //Gets user and their id.
            var user = await _userManager.GetUserAsync(User);
            var id = user?.Id;

            //Returns Not Found if User is not found.
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

            //Gets current user.
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
            
            //Returns all Claims of User.
            var allClaims = await _userManager.GetClaimsAsync(user);
            //Searches for the Name Claim.
            var userClaims = (from c in allClaims
                         where c.Type == "Name"
                         select c).ToList();
            
            //Removes them and replaces with The New Name.
            await _userManager.RemoveClaimsAsync(user, userClaims);
            await _userManager.AddClaimAsync(user, new Claim("Name", $"{newUser.FirstName} {newUser.LastName}"));

            //Returns the user to their user homepage after the changes.
            return RedirectToAction("Index", "Account");
        }

        //Function that adds books to Cart.
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddToCart(int? bookID, string controlla, string acta)
        {
            if(bookID == null)
            {
                return View("NotFound");
            }

            //Gets the user and their id.
            var user = await _userManager.GetUserAsync(User);
            var userID = user?.Id;

            //Creates the InputModel for the Cart.
            var newCartItem = new CartInputModel()
            {
                BookID = (int)bookID,
                UserID = userID,
            };
            //Adds this the cart connection from user to book.
            _cartService.AddCartItem(newCartItem);
            //Returns the user from where they came.
            if(acta == "Details")
            {
                return RedirectToAction(acta, controlla, new {id = bookID});
            }
            return RedirectToAction(acta, controlla);
        }

        //Function that adds books to the Wishlist.
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddToWishlist(int? bookID, string controlla, string acta)
        {
            if(bookID == null)
            {
                return View("NotFound");
            }

            //Waits half a second so the add to wishlist animation can finish.
            System.Threading.Thread.Sleep(1000);

            //Gets the user and their id
            var user = await _userManager.GetUserAsync(User);
            var userID = user?.Id;

            //Creates the InputModel for the Wishlist.
            var newWishlistItem = new WishlistInputModel((int)bookID, userID);
            //Adds the wishlist connection from user to book.
            _wishlistService.AddWishlistItem(newWishlistItem);
            //Redirects the user to the Catalogue.
            if(acta == "Details")
            {
                return RedirectToAction(acta, controlla, new {id = bookID});
            }
            return RedirectToAction(acta, controlla);
        }

        //Returns the Choose Avatar View. User is presented with different Avatars.
        [Authorize]
        public IActionResult ChooseAvatar()
        {
            return View();
        }

        //Receives the User Request for new Avatar Change.
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChooseAvatar(string image)
        {
            //Gets current User.
            var user = await _userManager.GetUserAsync(User);
            var userID = user.Id;
            //Changes Avatar to new Image.
            _userService.ChangeAvatar(userID, image);
            //Redirects to EditProfile View.
            return RedirectToAction("EditProfile", "Account");
        }

        //Takes a copy of the Cart and Writes it to Order.
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> MakeOrder()
        {
            //Gets Current User
            var user = await _userManager.GetUserAsync(User);
            var userID = user.Id;
            //Gets all Books in Cart
            var booksInCart = _cartService.GetCartBooks(userID);

            //Creates a new Order
            var newOrder = new OrderInputModel()
            {
                UserID = userID,
                Books = booksInCart,
            };

            //Checks if user has current order to see if we will be adding order or updating order.
            bool userHasCurrentOrder = _orderService.UserHasCurrentOrder(userID);

            //If User does not have a current order.
            if(!userHasCurrentOrder)
            {
                //Adds new Order
                _orderService.AddOrder(newOrder);
                var orderID = _orderService.GetCurrentOrderID(userID);
                //Adds new Order Book Connections for all books in cart.
                for(int i = 0; i < booksInCart.Count; i++)
                {
                    _obcService.AddConnection((int)orderID, booksInCart[i].ID, booksInCart[i].Amount);
                }
            }
            else
            {
                //Updates order with the new Cart.
                var orderID = _orderService.GetCurrentOrderID(userID);
                _orderService.UpdateOrder(newOrder, (int)orderID);
                //Updates order book connections with new amounts or new books.
                for(int i = 0; i < booksInCart.Count; i++)
                {
                    _obcService.UpdateConnection((int)orderID, booksInCart[i].ID, booksInCart[i].Amount);
                }
            }

            //Lastly returns User to The Check out window for order.
            return RedirectToAction("CheckOut", "Order");
        }

        //Adds final touches to Order.
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> OrderCheckOutToVerify(CheckoutInputModel model)
        {
            //Checks if input boxes were filled in.
            if(!ModelState.IsValid)
            {
                //Redirects back 
                return RedirectToAction("CheckOut", "Order");
            }
            //Gets Current user.
            var user = await _userManager.GetUserAsync(User);
            var userID = user.Id;
            
            //Updates the Order Input model to have the user ID
            model.UserID = userID;

            //Returns to user that they can Review Order.
            return RedirectToAction("Review", "Order", model);
        }

        //Sets Order to Confirmed
        [HttpPost]
        [Authorize]
        public IActionResult ConfirmedOrder(int orderID, bool wrapped)
        {
            //Confirms the Order and finalizes all order variables.
            _orderService.ConfirmOrder(orderID, wrapped);
            //Empties Cart to make space for new order.
            _cartService.DeleteCartFinalizeOrder(orderID);

            //Redirects to home page after order confirmation.
            return RedirectToAction("Index", "Home");
        }

        //Updates Cart when User updates amount in cart.
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateCart(int bookID, int amount)
        {
            //Gets Current user.
            var user = await _userManager.GetUserAsync(User);
            var userID = user.Id;
            //makes an updated cart connection.
            var model = new CartInputModel()
            {
                UserID = userID,
                BookID = bookID,
                Amount = amount,
            };
            //updates cart with new connection.
            _cartService.UpdateConnection(model);
            //redirects users to cart after the updating.
            return RedirectToAction("Cart", "Account");
        }

        //Removes Item from cart.
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> RemoveCart(int bookID)
        {
            //Gets Current User.
            var user = await _userManager.GetUserAsync(User);
            var userID = user.Id;
            //Removes cart item.
            _cartService.RemoveItem(userID, bookID);
            //Sees if User has an Order.
            bool userHasCurrentOrder = _orderService.UserHasCurrentOrder(userID);

            //If user has an order, we must delete the books from there as well.
            if(userHasCurrentOrder)
            {
                //Removes all Order Book Connections in the current order(which is a copy of cart.)
                var orderID = _orderService.GetCurrentOrderID(userID);
                _obcService.RemoveItem((int)orderID, bookID);
            }

            //Redirects user back to the cart.
            return RedirectToAction("Cart", "Account");
        }

        //Removes item from wishlist
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> RemoveWishlist(int bookID)
        {
            //Gets current User.
            var user = await _userManager.GetUserAsync(User);
            var userID = user.Id;

            //Removes the book from the user's wishlist.
            _wishlistService.RemoveItem(userID, bookID);

            //Redirects user back to wishlist.
            return RedirectToAction("Wishlist", "Account");
        }

        //Removes item from wishlist and moves it to cart.
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> MoveWishlistToCart(int bookID)
        {
            //Gets current User.
            var user = await _userManager.GetUserAsync(User);
            var userID = user.Id;

            //Removes Item from Wishlist...
            _wishlistService.RemoveItem(userID, bookID);
            //and creates a cart model for that book.
            var cartItem = new CartInputModel()
            {
                BookID = bookID,
                UserID = userID,
            };
            //Adds the new cart item to the cart.
            _cartService.AddCartItem(cartItem);

            //Redirects user to wishlist.
            return RedirectToAction("Wishlist", "Account");
        }

        //User adds Rating.
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddRating(int score, string comment, int bookID)
        {
            //Gets current User.
            var user = await _userManager.GetUserAsync(User);
            var userID = user.Id;

            //Sees if user filled in comment. Avoids null reference error when doing comment.Length.
            if(comment != null)
            {
                //Comment can not exceed 500.
                if(comment.Length > 500)
                {
                    //Returns user to the Rating Site to write again.
                    return RedirectToAction("Rating", "Book", new {id = bookID});
                }
            }

            //Creates a new Review model.
            var newReview = new RatingInputModel()
            {
                Score = score,
                Comment = comment,
                BookID = bookID,
                UserID = userID,
            };

            //Adds the rating itself. The comment which is at the bottom of the page.
            _ratingService.AddRating(newReview);
            //Updates the book star rating.
            _bookService.AddRating(newReview);

            //Redirects User to Details.
            return RedirectToAction("Details", "Book", new {id = bookID});
        }

        //Sets favorite book.
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> MakeFavorite(int bookID)
        {
            //Gets current user.
            var user = await _userManager.GetUserAsync(User);
            var userID = user.Id;

            //Waits for 0.3 seconds so the animation finishes when the user clisk the make favorite button
            System.Threading.Thread.Sleep(800);

            //Connects user to favorite book.
            _userService.MakeFavorite(userID, bookID);

            //Redirects to Details.
            return RedirectToAction("Details", "Book", new {id = bookID});
        }

        //Gets Order History for the user.
        [Authorize]
        public async Task<IActionResult> OrderHistory()
        {
            //Gets current user.
            var user = await _userManager.GetUserAsync(User);
            var userID = user.Id;
            //Gets the Order History for the user.
            var orders = _orderService.GetOrderHistory(userID);
            //Returns the OrderHistory with the model of orders.
            return View(orders);
        }

        //Adds Likes to a Comment.
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> VoteComment(int ratingID, int bookID)
        {
            //Gets Current User.
            var user = await _userManager.GetUserAsync(User);
            var userID = user.Id;
            //Adds vote to Rating.
            _ratingService.AddVote(userID, ratingID);
            //Redirects user to same page after voting.
            return RedirectToAction("Details", "Book", new {id = bookID});
        }

        //Adds new Book.
        [Authorize(Roles="staff")]
        public IActionResult AddBook()
        {
            //Gives the user the input form for the Book.
            return View();
        }

        //Receives new book.
        [HttpPost]
        [Authorize(Roles="staff")]
        public IActionResult AddBook(BookInputModel model)
        {
            //If not all fields are filled. Try again.
            if(!ModelState.IsValid)
            {
                return RedirectToAction("AddBook", "Account");
            }
            //Adds new book.
            _bookService.AddBook(model);

            //Redirects to user account.
            return RedirectToAction("Index", "Account");
        }

        //Removes rating.
        [HttpPost]
        [Authorize(Roles="staff")]
        public IActionResult RemoveRating(int bookID, int ratingID)
        {
            //Removes the rating.
            _ratingService.RemoveRating(ratingID);
            //Redirects to the Details view of the book where the comment/rating was.
            return RedirectToAction("Details", "Book", new {id = bookID});
        }

        //Gets the Orders that Employees can mark as shipped.
        [Authorize(Roles="staff")]
        public IActionResult Orders()
        {
            //Gets all ready orders
            var orders = _orderService.GetOrders();
            //Returns them in a view.
            return View(orders);
        }
    }
}