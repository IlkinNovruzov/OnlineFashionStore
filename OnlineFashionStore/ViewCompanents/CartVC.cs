using Microsoft.AspNetCore.Mvc;
using OnlineFashionStore.Extensions;
using OnlineFashionStore.Models.ViewModels;

namespace OnlineFashionStore.ViewCompanents
{
    public class CartVC : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            CartViewModel cartVM = new()
            {
                CartItems = cart,
                GrandTotal = cart.Sum(x => x.Quantity * x.Price)
            };
            return View(cartVM);
        }
    }
}
