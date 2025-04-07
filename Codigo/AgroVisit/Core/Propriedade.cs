using Microsoft.AspNetCore.Http;

namespace Core;

public partial class Propriedade
{
    public uint Id { get; set; }

    public string Nome { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public string Cidade { get; set; } = null!;

    public int? QuantFuncionario { get; set; }

    public float? AreaReserva { get; set; }

    public float? AreaPreservar { get; set; }

    public string? Car { get; set; }

    public string? Ccir { get; set; }

    public string? Itr { get; set; }

    public byte[]? Georreferenciamento { get; set; }

    public byte[]? MatriculaImovel { get; set; }

    public float? AreaTotal { get; set; }

    public float? AreaCultivada { get; set; }

    public string? Comercializacao { get; set; }

    public byte[]? HistoricoProdAgricola { get; set; }

    public byte[]? HistoricoFitossanidade { get; set; }

    public uint IdSolo { get; set; }

    public uint IdCultura { get; set; }

    public uint IdCliente { get; set; }

    public uint IdEngenheiroAgronomo { get; set; }

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual Cultura IdCulturaNavigation { get; set; } = null!;

    public virtual Engenheiroagronomo IdEngenheiroAgronomoNavigation { get; set; } = null!;

    public virtual Solo IdSoloNavigation { get; set; } = null!;

    public virtual ICollection<Projeto> Projetos { get; set; } = new List<Projeto>();

    public virtual ICollection<Visita> Visita { get; set; } = new List<Visita>();
}
