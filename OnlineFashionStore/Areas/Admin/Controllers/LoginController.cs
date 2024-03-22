using Microsoft.AspNetCore.Mvc;

namespace OnlineFashionStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
