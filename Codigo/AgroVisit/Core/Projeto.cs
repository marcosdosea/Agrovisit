using System;
using System.Collections.Generic;

namespace Core;

public partial class Projeto
{
    public uint Id { get; set; }

    public string Nome { get; set; } = null!;

    public float Valor { get; set; }

    public uint QuantParcela { get; set; }

    public DateTime DataInicio { get; set; }

    public DateTime DataPrevista { get; set; }

    public string? Descricao { get; set; }

    public byte[]? Anexo { get; set; }

    public string Status { get; set; } = null!;

    public uint IdPropriedade { get; set; }

    public int? NumeroVisita { get; set; }

    public DateTime? DataConclusao { get; set; }

    public virtual ICollection<Conta> Conta { get; set; } = new List<Conta>();

    public virtual Propriedade IdPropriedadeNavigation { get; set; } = null!;

    public virtual ICollection<Intervencao> Intervencoes { get; set; } = new List<Intervencao>();
}
