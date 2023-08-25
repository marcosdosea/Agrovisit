namespace Core.DTO
{
    public partial class VisitaDTO
    {
        public uint Id { get; set; }

        public string? Observacoes { get; set; }

        public DateTime DataHora { get; set; }

        public string Status { get; set; } = null!;

        public uint IdPropriedade { get; set; }

        public virtual ICollection<ContaDTO> Conta { get; set; } = new List<ContaDTO>();

        public virtual Propriedade IdPropriedadeNavigation { get; set; } = null!;
    }
}
