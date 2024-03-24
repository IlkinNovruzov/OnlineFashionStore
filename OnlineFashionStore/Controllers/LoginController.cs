using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineFashionStore.Models;
using OnlineFashionStore.Models.DataModels;
using OnlineFashionStore.Models.ViewModels;

namespace OnlineFashionStore.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public LoginController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(AppUserLogin appUserLogin)
        {
            var result = await _signInManager.PasswordSignInAsync(appUserLogin.Username, appUserLogin.Password, false, true);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(appUserLogin.Username);
                    return RedirectToAction("Index", "Home");
                if (user.EmailConfirmed == true)
                {
                }
            }
            return View();
        }
    }
}
