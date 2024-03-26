using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace OnlineFashionStore.Controllers
{
    [Authorize]
    public class SaleController : Controller
    {
        public IActionResult Checkout()
        {
            return View();
        }
        
    }
}
