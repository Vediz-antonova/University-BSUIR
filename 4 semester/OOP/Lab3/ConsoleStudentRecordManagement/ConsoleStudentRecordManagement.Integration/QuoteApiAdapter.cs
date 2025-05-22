using System.Text.Json;
using ConsoleStudentRecordManagement.DTO;
namespace ConsoleStudentRecordManagement.Integration;

public class QuoteApiAdapter : IQuoteApiAdapter
{
    private readonly HttpClient _httpClient;

    public QuoteApiAdapter()
    {
        var handler = new HttpClientHandler { ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator };
        _httpClient = new HttpClient(handler);
    }

    public async Task<QuoteDTO> GetMotivationalQuoteAsync()
    {
        var response = await _httpClient.GetAsync("https://api.quotable.io/random");
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        var quoteFromApi = JsonSerializer.Deserialize<QuoteDTO>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        
        if(quoteFromApi != null)
        {
            return DTOFactory.CreateQuoteDTO(quoteFromApi.Content, quoteFromApi.Author);
        }
        return null;
    }
}