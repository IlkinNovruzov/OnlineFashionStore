using Microsoft.AspNetCore.Mvc;

namespace BankAccountApi.Controllers
{
    public class PaymentController : Controller
    {
        private readonly BankAccountService _bankAccountService;

        public PaymentController(BankAccountService bankAccountService)
        {
            _bankAccountService = bankAccountService;
        }

        [HttpPost("payment/{cardId}/{amount}")]
        public IActionResult MakePayment(int cardId, decimal amount)
        {
            // Ödeme işlemleri burada gerçekleştirilecek
            var paymentResult = _bankAccountService.MakePayment(cardId, amount);

            if (paymentResult.IsCompletedSuccessfully)
            {
                return Ok();
            }
            else
            {
                return StatusCode(500, "Ödeme işlemi başarısız oldu.");
            }
        }

    }
}
