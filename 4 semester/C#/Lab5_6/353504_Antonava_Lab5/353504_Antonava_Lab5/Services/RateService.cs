using System.Text.Json;
using _353504_Antonava_Lab5.Entities;
namespace _353504_Antonava_Lab5.Services;

public class RateService : IRateService
{
    private readonly HttpClient _httpClient;
    private readonly List<string> _currencies = new List<string> { "RUB", "EUR", "USD", "CHF", "CNY", "GBP" };

    public RateService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Rate>> GetRates(DateTime date)
    {
        var rates = new List<Rate>();

        foreach (var currency in _currencies)
        {
            string url = $"{currency}?ondate={date:yyyy-MM-dd}&parammode=2";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var rate = JsonSerializer.Deserialize<Rate>(json);
                if (rate != null)
                {
                    rates.Add(rate);
                }
            }
        }
        return rates;
    }
}
