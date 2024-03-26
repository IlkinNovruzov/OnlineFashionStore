using Microsoft.AspNetCore.Mvc;

namespace BankAccountApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BankAccountController : Controller
    {
        [HttpPost]
        [Route("MakePayment/{cardId}/{amount}")]
        public IActionResult MakePayment(int cardId, decimal amount)
        {
            return Ok($"Payment made for cardId: {cardId}, amount: {amount}");
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
