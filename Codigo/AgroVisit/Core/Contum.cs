using System;
using System.Collections.Generic;

namespace Core;

public partial class Contum
{
    public uint Id { get; set; }

    public float Valor { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? DataPagamento { get; set; }

    public uint IdProjeto { get; set; }

    public uint IdVisita { get; set; }

    public virtual Projeto IdProjetoNavigation { get; set; } = null!;

    public virtual Visitum IdVisitaNavigation { get; set; } = null!;
}
