using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TheBookCave.Models;
using TheBookCave.Models.InputModels;
using TheBookCave.Services;

namespace TheBookCave.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        private BookService _bookService;
        private UserService _userService;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _bookService = new BookService();
            _userService = new UserService();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterInputModel model)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            var user = new ApplicationUser { UserName = model.Email, Email = model.Email};

            var result = await _userManager.CreateAsync(user, model.Password);

            if(result.Succeeded)
            {
                //The user is successfully registered
                //Add the concatenated first and last name as fullName in claims
                await _userManager.AddClaimAsync(user, new Claim("Name", $"{model.FirstName} {model.LastName}"));
                await _signInManager.SignInAsync(user, false);

                _userService.AddUser(model);

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginInputModel model)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            
            if(result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        //Everything above this line is from the Authorization lectures.
        //Everything below this line will be the other methods

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Cart()
        {
            var user = await GetCurrentUserAsync();
            var id = user?.Id;

            var books = _bookService.GetCartBooks(id);

            return View(books);
        }

        [Authorize]
        public async Task<IActionResult> Wishlist()
        {
            var user = await GetCurrentUserAsync();
            var id = user?.Id;

            var books = _bookService.GetWishlistBooks(id);

            return View(books);
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        [Authorize]
        public IActionResult EditProfile()
        {
            //Get User View Model
            return View();
        }
    }
}