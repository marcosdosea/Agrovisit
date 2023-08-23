using System;
using System.Collections.Generic;

namespace Core;

public partial class Visitum
{
    public uint Id { get; set; }

    public string? Observacoes { get; set; }

    public DateTime DataHora { get; set; }

    public string Status { get; set; } = null!;

    public uint IdPropriedade { get; set; }

    public virtual ICollection<Contum> Conta { get; set; } = new List<Contum>();

    public virtual Propriedade IdPropriedadeNavigation { get; set; } = null!;
}
