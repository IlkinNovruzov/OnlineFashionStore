using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OnlineFashionStore.Controllers
{
    public class ShoppingCartController : Controller
    {
        public IActionResult AddToSession()
        {
            ISession session = HttpContext.Session;
            session.SetString("testKey", "Değer"); 
            return Ok("Değer başarıyla eklendi.");
            // List<int> cart = session.Get<List<int>>("Cart") ?? new List<int>();
        }
    }
}
