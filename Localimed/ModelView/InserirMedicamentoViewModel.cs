using Localimed.Model;
using Localimed.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;


namespace Localimed.ModelView;

public class InserirMedicamentoViewModel : INotifyPropertyChanged
{
    private string _nomeMedicamento = string.Empty;
    private string _quantidadeMedicamento = string.Empty;
    private string _numeroLote = string.Empty;
    private string _tipoMedicamento = string.Empty;
    private string _dosagemMedicamento = string.Empty;
    private DateTime _dataMedicamento = DateTime.Today;


    public string NomeMedicamento
    {
        get => _nomeMedicamento;
        set { if (_nomeMedicamento != value) { _nomeMedicamento = value; OnPropertyChanged(); } }
    }

    public string DosagemMedicamento
    {
        get => _dosagemMedicamento;
        set { if (_dosagemMedicamento != value) { _dosagemMedicamento = value; OnPropertyChanged(); } }
    }

    public string QuantidadeMedicamento
    {
        get => _quantidadeMedicamento;
        set
        {
            var cleaned = new string((value ?? string.Empty).Where(char.IsDigit).ToArray());
            if (_quantidadeMedicamento != cleaned)
            {
                _quantidadeMedicamento = cleaned;
                OnPropertyChanged();
            }
        }
    }

    public string NumeroLote
    {
        get => _numeroLote;
        set
        {
            var cleaned = new string((value ?? string.Empty).Where(char.IsDigit).ToArray());
            if (_numeroLote != cleaned)
            {
                _numeroLote = cleaned;
                OnPropertyChanged();
            }
        }
    }

    public string TipoMedicamento
    {
        get => _tipoMedicamento;
        set { if (_tipoMedicamento != value) { _tipoMedicamento = value; OnPropertyChanged(); } }
    }

    public DateTime DataMedicamento
    {
        get => _dataMedicamento;
        set { if (_dataMedicamento != value) { _dataMedicamento = value; OnPropertyChanged(); } }
    }

    public ICommand BotaoConfirmar { get; }
    public ICommand BotaoVoltar { get; }


    private readonly MedicamentoApiService _api;

    public InserirMedicamentoViewModel(
        MedicamentoApiService api)
    {
        _api = api;

        BotaoConfirmar = new Command(async () => await ConfirmarAsync());
        BotaoVoltar = new Command(async () => await VoltarAsync());
    }

    private async Task ConfirmarAsync()
    {
        if (string.IsNullOrWhiteSpace(NomeMedicamento))
        {
            await Alert("Erro", "O nome do medicamento está vazio.");
            return;
        }

        if (!int.TryParse(QuantidadeMedicamento, out var quantidade) || quantidade <= 0)
        {
            await Alert("Erro", "A quantidade deve ser um número maior que zero.");
            return;
        }

        if (!int.TryParse(NumeroLote, out var lote) || lote <= 0)
        {
            await Alert("Erro", "O número do lote deve ser um número maior que zero.");
            return;
        }

        if (string.IsNullOrWhiteSpace(TipoMedicamento))
        {
            await Alert("Erro", "Selecione um tipo de medicamento.");
            return;
        }

        float.TryParse(
            DosagemMedicamento.Replace(',', '.'),
            System.Globalization.NumberStyles.Float,
            System.Globalization.CultureInfo.InvariantCulture,
            out var dosagem);

        var dto = new CriarMedicamentoDto
        {
            NomeMedicamento = NomeMedicamento.Trim(),
            Dosagem = (decimal)dosagem,
            Quantidade = quantidade
        };

        var sucesso =
              await _api.CriarMedicamentoAsync(dto);
            if (!sucesso)
        {

            await Alert(
                "Erro",
                "Medicamento não foi salvo na API");
            return;

        }

        await Alert(
            "Cadastro Realizado",
            $"Medicamento '{dto.NomeMedicamento}' cadastrado com sucesso.");

        NomeMedicamento = string.Empty;
        DosagemMedicamento = string.Empty;
        QuantidadeMedicamento = string.Empty;
        NumeroLote = string.Empty;
        TipoMedicamento = string.Empty;
        DataMedicamento = DateTime.Today;
    }

    private static Task Alert(string title, string message) =>
        Application.Current!.MainPage!.DisplayAlert(title, message, "OK");

    private async Task VoltarAsync()
    {
        if (Application.Current?.MainPage?.Navigation.NavigationStack.Count > 1)
            await Application.Current.MainPage.Navigation.PopAsync();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? name = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
