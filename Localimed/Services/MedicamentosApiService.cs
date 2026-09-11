using Localimed.Model;
using System.Net.Http.Json;

namespace Localimed.Services;

public class MedicamentoApiService
{
    private readonly HttpClient _httpClient;

    public async Task<bool> CriarMedicamentoAsync(
    CriarMedicamentoDto medicamento)
    {
        var response = await _httpClient
            .PostAsJsonAsync(
                "api/medicamentos",
                medicamento);

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> ExcluirMedicamentoAsync(
    int idMedicamento)
    {
        var response =
            await _httpClient.DeleteAsync(
                $"api/medicamentos/{idMedicamento}");

        return response.IsSuccessStatusCode;
    }

    public MedicamentoApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<MedicamentoApi>> ObterMedicamentosAsync()
    {
        return await _httpClient
            .GetFromJsonAsync<List<MedicamentoApi>>(
                "api/medicamentos")
            ?? new List<MedicamentoApi>();

    }
}
