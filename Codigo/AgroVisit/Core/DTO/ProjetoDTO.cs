namespace Core.DTO
{
    public partial class ProjetoDTO
    {
        public uint Id { get; set; }

        public float Valor { get; set; }

        public uint QuantParcela { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime DataPrevista { get; set; }

        public string? Descricao { get; set; }

        public byte[]? Anexo { get; set; }

        public string Status { get; set; } = null!;

        public uint IdPropriedade { get; set; }

        public virtual ICollection<ContaDTO> Conta { get; set; } = new List<ContaDTO>();

        public virtual PropriedadeDTO IdPropriedadeNavigation { get; set; } = null!;

        public virtual ICollection<Intervencao> Intervencaos { get; set; } = new List<Intervencao>();
    }

}