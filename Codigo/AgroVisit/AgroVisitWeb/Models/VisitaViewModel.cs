using Core;
using System.ComponentModel.DataAnnotations;

namespace AgroVisitWeb.Models
{
    public class VisitaViewModel
    {
        public uint Id { get; set; }

        [StringLength(200)]
        public string? Observacoes { get; set; }

        [Display(Name = "Data e horário:")]
        [DataType(DataType.DateTime, ErrorMessage = "É necessário escolher uma data válida.")]
        [Required(ErrorMessage = "Campo obrigatório")]        
        public DateTime DataHora { get; set; }
	
		[Required(ErrorMessage = "Campo obrigatório")]
        public string Status { get; set; } = null!;

		[Required(ErrorMessage = "Campo obrigatório")]
        public uint IdPropriedade { get; set; }
    }
}
