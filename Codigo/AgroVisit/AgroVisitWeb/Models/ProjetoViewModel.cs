using Core;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AgroVisitWeb.Models
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
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataInicio { get; set; }

        [Display(Name = "Data da primeira parcela")]
        [Required(ErrorMessage = "O campo é obrigatótio.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataPrevista { get; set; }

        [Display(Name = "Descrição")]
        [StringLength(200)]
        public string? Descricao { get; set; }

        [Display(Name = "Número de visitas")]
        public int? NumeroVisita { get; set; }

        [Display(Name = "Anexo")]
        public byte[]? Anexo { get; set; }

        [Display(Name = "Data de conclusão")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime dataConclusao { get; set; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "O campo é obrigatótio.")]
        public string Status { get; set; } = null!;

        [Display(Name = "Propriedade")]
        [Required(ErrorMessage = "O campo é obrigatótio.")]
        public uint IdPropriedade { get; set; }

        [Display(Name = "Propriedade")]
        public SelectList? ListaPropriedades { get; set; }

        [Display(Name = "Conta")]
        public IEnumerable<Conta>? ListaContas { get; set; }

        [Display(Name = "Intervenções")]
        public IEnumerable<Intervencao>? ListaIntervencoes { get; set; }
    }
}
