using Localimed.ModelView;
using Localimed.Services;

namespace Localimed.Views;

public partial class VisualizarEstoque : ContentPage
{
    private readonly MedicamentoApiService _api;

    public VisualizarEstoque()
    {
        InitializeComponent();

        BindingContext = new VisualizarEstoqueViewModel();

        _api = new MedicamentoApiService(
            new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7140/")
            });
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        try
        {
            var resposta = await _api.TestarConexaoAsync();

            await DisplayAlert(
                "API OK",
                resposta,
                "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert(
                "ERRO",
                ex.Message,
                "OK");
        }
    }
}
