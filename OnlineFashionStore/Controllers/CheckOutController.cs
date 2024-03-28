using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Stripe.Checkout;
using OnlineFashionStore.Models.DataModels;
namespace OnlineFashionStore.Controllers
{
    [Authorize]
    public class CheckOutController : Controller
    {
        public IActionResult Checkout()
        {
            var list = new List<Product>()
            {
                new Product
                {
                    Name="sss",
                    Price=233,
                    StockQuantity=2
                }
            };
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

                        UnitAmount = (long)(item.Price * item.StockQuantity),
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Name.ToString()
                        }
                    },
                    Quantity = item.StockQuantity
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
