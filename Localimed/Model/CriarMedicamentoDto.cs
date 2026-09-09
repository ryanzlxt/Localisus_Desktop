using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Localimed.Model;

public class CriarMedicamentoDto
{
    public string NomeMedicamento { get; set; } = string.Empty;

    public decimal Dosagem { get; set; }

    public int Quantidade { get; set; }
}
