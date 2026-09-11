using Localimed.ModelView;
using Localimed.Services;

namespace Localimed.Views;

public partial class RemoverMedicamentos : ContentPage
{
    public RemoverMedicamentos()
    {
        InitializeComponent();

        BindingContext = new RemoverMedicamentoViewModel();
    }
}