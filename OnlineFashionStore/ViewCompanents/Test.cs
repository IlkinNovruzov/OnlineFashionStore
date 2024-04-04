using Microsoft.AspNetCore.Mvc;
using OnlineFashionStore.Extensions;
using OnlineFashionStore.Models;
using OnlineFashionStore.Models.ViewModels;

namespace OnlineFashionStore.ViewCompanents
{
    public class Test : ViewComponent
    {
        private readonly AppDbContext _context;
        public Test(AppDbContext context)
        {
            _context = context;

        }
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
