namespace Localimed.Services;

public class MedicamentoApiService
{
    private readonly HttpClient _httpClient;

    public MedicamentoApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> TestarConexaoAsync()
    {
        return await _httpClient.GetStringAsync(
            "api/medicamentos");
    }
}
