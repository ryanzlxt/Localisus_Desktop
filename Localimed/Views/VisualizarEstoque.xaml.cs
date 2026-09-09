using Localimed.ModelView;
using Localimed.Services;

namespace Localimed.Views;

public partial class VisualizarEstoque : ContentPage
{
    public VisualizarEstoque()
    {
        InitializeComponent();

        var service = new MedicamentoApiService(
            new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7140/")
            });

        BindingContext =
            new VisualizarEstoqueViewModel(service);
    }
}