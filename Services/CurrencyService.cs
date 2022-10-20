using System.Reflection.Metadata.Ecma335;
using Models;
using Newtonsoft.Json;

namespace Services;

public class CurrencyService
{
    public async Task<Currency> ConvertCurrency(Currency currency)
    {
        Response response;
        using (var client = new HttpClient())
        {
            HttpResponseMessage responseMessage = await client.GetAsync(
                "https://www.amdoren.com/api/currency.php?api_key=j37SFVN4TCBV8H7pLPm2WpkVYSFTDY&from=EUR&to=GBP&amount=50");
            responseMessage.EnsureSuccessStatusCode();

            var message = await responseMessage.Content.ReadAsStringAsync();
            response = JsonConvert.DeserializeObject<Response>(message);
        }

        return currency;
    }
}