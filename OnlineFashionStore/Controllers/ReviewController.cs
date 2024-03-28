using Microsoft.AspNetCore.Mvc;

namespace OnlineFashionStore.Controllers
{
    public class ReviewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
