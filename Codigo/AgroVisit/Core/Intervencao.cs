using System;
using System.Collections.Generic;

namespace Core;

public partial class Intervencao
{
    public uint Id { get; set; }

    public string Pratica { get; set; } = null!;

    public string? Descricao { get; set; }

    public DateTime? DataAplicacao { get; set; }

    public string? TipoProduto { get; set; }

    public float? AreaTratada { get; set; }

    public string Status { get; set; } = null!;

    public uint IdProjeto { get; set; }

    public virtual Projeto IdProjetoNavigation { get; set; } = null!;

   
}
