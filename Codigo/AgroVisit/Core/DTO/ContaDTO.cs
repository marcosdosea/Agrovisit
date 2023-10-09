namespace Core.DTO
{
    public partial class ContaDTO
    {
        public uint Id { get; set; }

        public float Valor { get; set; }

        public string Status { get; set; } = null!;

        public DateTime? DataPagamento { get; set; }

        public uint IdProjeto { get; set; }

        public uint IdVisita { get; set; }

        public virtual ProjetoDto IdProjetoNavigation { get; set; } = null!;

        public virtual VisitaDTO IdVisitaNavigation { get; set; } = null!;
    }
}
