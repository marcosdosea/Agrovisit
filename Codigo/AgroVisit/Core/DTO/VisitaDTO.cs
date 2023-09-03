using System.ComponentModel;

namespace Core.DTO
{
    public partial class VisitaDTO
    {
        public uint Id { get; set; }
        [DisplayName("Observações:")]
        public string? Observacoes { get; set; }
        [DisplayName("Data e horário:")]
        public DateTime DataHora { get; set; }

        public string Status { get; set; } = null!;
    }
}
