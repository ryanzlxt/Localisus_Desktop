namespace Localimed.Model;

public class MedicamentoApi
{
    public int IdMedicamento { get; set; }

    public string NomeMedicamento { get; set; } = string.Empty;

    public decimal Dosagem { get; set; }

    public int Quantidade { get; set; }
}
