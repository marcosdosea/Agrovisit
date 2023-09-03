using Core;
using System.ComponentModel.DataAnnotations;

namespace AgroVisitWeb.Models
{
    public class VisitaModel
    {
        public uint Id { get; set; }

        [StringLength(200)]
        public string? Observacoes { get; set; }

        [Display(Name = "Data e horário:")]
        [DataType(DataType.Date, ErrorMessage = "É necessário escolher uma data válida.")]
        [Required(ErrorMessage = "Campo obrigatório")]        
        public DateTime DataHora { get; set; }

        public string Status { get; set; } = null!;

        public uint IdPropriedade { get; set; }
    }
}
