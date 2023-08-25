namespace Core.DTO
{
    public partial class PropriedadeDTO
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

        public int? NumAnimais { get; set; }

        public string? Raca { get; set; }

        public string? FonteAlimento { get; set; }

        public float? AreaPasto { get; set; }

        public byte[]? HistoricoProducao { get; set; }

        public float? AreaTotal { get; set; }

        public float? AreaCultivada { get; set; }

        public string? TipoSolo { get; set; }

        public string? Cultura { get; set; }

        public string? Comercializacao { get; set; }

        public byte[]? HistoricoProdAgricola { get; set; }

        public byte[]? HistoricoFitossanidade { get; set; }

        public uint IdSolo { get; set; }

        public uint IdCultura { get; set; }

        public uint IdCliente { get; set; }

        public uint IdEngenheiroAgronomo { get; set; }

        public virtual ClienteDTO IdClienteNavigation { get; set; } = null!;

        public virtual Cultura IdCulturaNavigation { get; set; } = null!;

        public virtual Engenheiroagronomo IdEngenheiroAgronomoNavigation { get; set; } = null!;

        public virtual SoloDTO IdSoloNavigation { get; set; } = null!;

        public virtual ICollection<Projeto> Projetos { get; set; } = new List<Projeto>();

        public virtual ICollection<VisitaDTO> Visita { get; set; } = new List<VisitaDTO>();
    }
}
