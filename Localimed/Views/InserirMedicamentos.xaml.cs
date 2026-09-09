using Localimed.ModelView;
using Localimed.Services;

namespace Localimed.Views;

public partial class InserirMedicamentos : ContentPage
{
    public InserirMedicamentos()
    {
        InitializeComponent();

        BindingContext =
            new InserirMedicamentoViewModel(
                new MedicamentoApiService(
                    new HttpClient
                    {
                        BaseAddress =
                            new Uri("https://localhost:7140/")
                    }));

        tipoMedicamentoPicker.ItemsSource = new[]
        {
            "Asma",
            "Diabetes",
            "Hipertensão",
            "Osteoporose",
            "Glaucoma",
            "Dor",
            "Neurológico",
            "Depressão",
            "Controlado"
        };
    }
}