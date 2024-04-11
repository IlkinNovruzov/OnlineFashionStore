using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Stripe.Checkout;
using OnlineFashionStore.Models.DataModels;
using OnlineFashionStore.Models.ViewModels;
using OnlineFashionStore.Extensions;
using OnlineFashionStore.Models;
using Microsoft.AspNetCore.Identity;
using Stripe.Climate;
using Microsoft.EntityFrameworkCore;

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
        private static Models.DataModels.Order tempOrder;
        private static decimal total=0;
        [HttpPost]
        public IActionResult Checkout(Models.DataModels.Order order)
        {
            var list = HttpContext.Session.GetJson<List<CartItem>>("Cart");
            tempOrder = order;
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
                        UnitAmountDecimal = item.Total*100,
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.ProductName.ToString()
                        }
                    },
                    Quantity = item.Quantity
                };
                total += item.Total;
                options.LineItems.Add(sessionListItem);
            }
            var service = new SessionService();
            Session session = service.Create(options);
            TempData["Session"] = session.Id;
            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }
        [HttpGet]
        public IActionResult Billing()
        {
            return View();
        }
        public async Task<IActionResult> OrderConfirmation()
        {
            var service = new SessionService();
            Session session = service.Get(TempData["Session"].ToString());
            if (session.PaymentStatus == "paid")
            {
                var user = await _userManager.GetUserAsync(User);
                var random = new Random();
                tempOrder.UserId = user.Id;
                tempOrder.OrderNumber = random.Next(100000, 1000000);
                tempOrder.Date = DateTime.Now;
                tempOrder.Total = total;

                _context.Orders.Add(tempOrder);
                _context.SaveChanges();
                var cartList = HttpContext.Session.GetJson<List<CartItem>>("Cart");
                foreach (var item in cartList)
                {
                    var orderDetail = new OrderItem
                    {
                        OrderId = tempOrder.Id,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        Subtotal = item.Total,
                        Color = item.Color,
                        Size = item.Size
                    };
                    _context.OrderItems.Add(orderDetail);
                    var product=_context.Products.FirstOrDefault(p=>p.Id==item.ProductId);
                    product.StockQuantity -= item.Quantity;
                }
                _context.SaveChanges();
                var _order = _context.Orders.Include(x => x.OrderItems).ThenInclude(x=>x.Product).FirstOrDefault(x=>x.Id==tempOrder.Id);
                return View("Success",_order);
            }
            return View("Fail");
        }



    }
}
