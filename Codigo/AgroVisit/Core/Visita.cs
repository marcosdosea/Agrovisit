using System;
using System.Collections.Generic;

namespace Core;

public partial class Visita
{
    public uint Id { get; set; }

    public string? Observacoes { get; set; }

    public DateTime DataHora { get; set; }

    public string Status { get; set; } = null!;

    public uint IdPropriedade { get; set; }

    public virtual ICollection<Conta> Contas { get; set; } = new List<Conta>();

    public virtual Propriedade IdPropriedadeNavigation { get; set; } = null!;
}
