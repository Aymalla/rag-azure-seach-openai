
namespace Frontend.Services;

public sealed class ApiClient(HttpClient httpClient)
{
    private readonly HttpClient _httpClient = httpClient;
    
}