using Microsoft.AspNetCore.Mvc;
using OnlineFashionStore.Extensions;
using OnlineFashionStore.Models.ViewModels;
namespace OnlineFashionStore.ViewCompanents
{
    public class NavbarVC : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");
            int cartCount = (cart == null) ? 0 : cart.Count;
            return View(cartCount);
        }
    }
}
