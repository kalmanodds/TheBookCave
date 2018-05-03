using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TheBookCave.Models;
using TheBookCave.Models.InputModels;

namespace TheBookCave.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
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
    }
}