using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AgroVisitAPI.Models
{
    public class ProjetoViewModel
    {
        [Key]
        public uint Id { get; set; }

        [Display(Name = "Nome do projeto")]
        [Required(ErrorMessage = "O campo é obrigatótio.")]
        [StringLength(50)]
        public string Nome { get; set; } = null!;

        [Display(Name = "Cliente")]
        public string? NomeCliente { get; set; }

        [Display(Name = "Propriedade")]
        public string? NomePropriedade { get; set; }

        [Display(Name = "Valor")]
        [Required(ErrorMessage = "O campo é obrigatótio.")]
        public float Valor { get; set; }

        [Display(Name = "Quantidade de parcelas")]
        [Required(ErrorMessage = "O campo é obrigatótio.")]
        public uint QuantParcela { get; set; }

        [Display(Name = "Data de inicio")]
        [Required(ErrorMessage = "O campo é obrigatótio.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataInicio { get; set; }

        [Display(Name = "Data da parcela")]
        [Required(ErrorMessage = "O campo é obrigatótio.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataPrevista { get; set; }

        [Display(Name = "Número de visitas")]
        public int? NumeroVisita { get; set; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "O campo é obrigatótio.")]
        public string Status { get; set; } = null!;

        [Display(Name = "Propriedade")]
        public SelectList? ListaPropriedades { get; set; }
    }
}
