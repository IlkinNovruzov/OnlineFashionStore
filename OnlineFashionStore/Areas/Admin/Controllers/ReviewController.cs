using Microsoft.AspNetCore.Mvc;

namespace OnlineFashionStore.Areas.Admin.Controllers
{
    public class ReviewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
