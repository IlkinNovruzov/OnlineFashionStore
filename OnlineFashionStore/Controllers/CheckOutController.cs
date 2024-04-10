using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Stripe.Checkout;
using OnlineFashionStore.Models.DataModels;
using OnlineFashionStore.Models.ViewModels;
using OnlineFashionStore.Extensions;
using OnlineFashionStore.Models;
using Microsoft.AspNetCore.Identity;

namespace OnlineFashionStore.Controllers
{
    [Authorize]
    public class CheckOutController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public CheckOutController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Checkout()
        {
            var list = HttpContext.Session.GetJson<List<CartItem>>("Cart");
           
            var domain = "https://localhost:44325/";
            var options = new SessionCreateOptions()
            {
                SuccessUrl = domain + $"CheckOut/OrderConfirmation",
                CancelUrl = domain + "CheckOut/Login",
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment"
            };
            foreach (var item in list)
            {
                var sessionListItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmountDecimal=item.Price * item.Quantity,
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.ProductName.ToString()
                        }
                    },
                    Quantity = item.Quantity
                };
                options.LineItems.Add(sessionListItem);
            }
            var service=new SessionService();
            Session session = service.Create(options);
            TempData["Session"] = session.Id;
            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }
        public IActionResult Billing()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> OrderConfirmation(Order order)
        {
            var service = new SessionService();
            Session session = service.Get(TempData["Session"].ToString());
            if (session.PaymentStatus == "Paid")
            {
                var user = await _userManager.GetUserAsync(User);
                order.UserId = user.Id;
                order.Date = DateTime.Now;

                _context.Orders.Add(order);
                _context.SaveChanges();

                var list = HttpContext.Session.GetJson<List<CartItem>>("Cart");
                foreach (var item in list)
                {
                    var orderDetail = new OrderItem
                    {
                        OrderId = order.Id,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        Subtotal = item.Price,
                        Color=item.Color,
                        Size=item.Size
                    };
                    _context.OrderItems.Add(orderDetail);
                }
                _context.SaveChanges();
                return View("Success");
            }
            return View("Login");
        }

        //public IActionResult OrderSuccess()


    }
}
