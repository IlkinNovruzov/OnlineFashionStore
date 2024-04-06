using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Stripe.Checkout;
using OnlineFashionStore.Models.DataModels;
using OnlineFashionStore.Models.ViewModels;
using OnlineFashionStore.Extensions;
namespace OnlineFashionStore.Controllers
{
    [Authorize]
    public class CheckOutController : Controller
    {
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

                        UnitAmount = (long)(item.Price * item.Quantity),
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
        public IActionResult OrderConfirmation()
        {
            var service = new SessionService();
            Session session = service.Get(TempData["Session"].ToString());
            if (session.PaymentStatus == "Paid")
            {
                return View("Success");
            }
            return View("Login");


        }

    }
}
