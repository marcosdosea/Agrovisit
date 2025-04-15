using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AgroVisitWeb.Models
{
    public class VisitaViewModel
    {
        [Required]
        [Key]
        public uint Id { get; set; }

        [Display(Name = "Observações")]
        [StringLength(200)]
        public string? Observacoes { get; set; }

        [Display(Name = "Data e hora")]
        [Required(ErrorMessage = "Campo obrigatório")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime DataHora { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Status { get; set; } = null!;

        [Display(Name = "Propriedade")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public uint IdPropriedade { get; set; }

        [Display(Name = "Propriedade")]
        public SelectList? ListaPropriedades { get; set; }

        [Display(Name = "Propriedade")]
        public string? NomePropriedade { get; set; }

        [Display(Name = "Cliente")]
        public string? NomeCliente { get; set; }
    }
}
