using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineFashionStore.Models.DataModels;

namespace OnlineFashionStore.Areas.Admin.Controllers
{
        [Area("Admin")]
    public class UserController : Controller
    {
            private readonly UserManager<AppUser> _userManager;
            private readonly RoleManager<AppRole> _roleManager;

            public UserController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
            {
                _userManager = userManager;
                _roleManager = roleManager;
            }

            public IActionResult GetUser()
            {
                return View(_userManager.Users.ToList());
            }

    }
}
