namespace BankAccountApi.Controllers
{
    public class BankAccountService
    {
        private readonly HttpClient _httpClient;

        public BankAccountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> MakePayment(int cardId, decimal amount)
        {
            // BankAccountApi'ye ödeme yapma isteği gönder
            var response = await _httpClient.PostAsync($"bankaccount/payment/{cardId}/{amount}", null);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
