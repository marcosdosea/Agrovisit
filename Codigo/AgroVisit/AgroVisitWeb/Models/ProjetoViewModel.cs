using Core;
using System.ComponentModel.DataAnnotations;

namespace AgroVisitWeb.Models
{
    public class ProjetoViewModel
    {
        public uint Id { get; set; }

        [Display(Name = "Nome do projeto")]
        [Required(ErrorMessage = "O campo é obrigatótio.")]
        [StringLength(50)]
        public string Nome { get; set; } = null!;

        [Required(ErrorMessage = "O campo é obrigatótio.")]
        public float Valor { get; set; }

        [Display(Name = "Parcelas")]
        [Required(ErrorMessage = "O campo é obrigatótio.")]
        public uint QuantParcela { get; set; }

        [Display(Name = "Data de inicio")]
        [Required(ErrorMessage = "O campo é obrigatótio.")]
        [DataType(DataType.Date)]
        public DateTime DataInicio { get; set; }

        [Display(Name = "Data da parcela")]
        [Required(ErrorMessage = "O campo é obrigatótio.")]
        [DataType(DataType.Date)]
        public DateTime DataPrevista { get; set; }

        [Display(Name = "Descrição")]
        [StringLength (200)]
        public string? Descricao { get; set; }

        [Display(Name = "Anexo")]
        public byte[]? Anexo { get; set; }

        [Display(Name = "Status")]
        [StringLength(2)]
        [Required(ErrorMessage = "O campo é obrigatótio.")]
        public string Status { get; set; } = null!;

        [Display(Name = "Propriedade")]
        [Required(ErrorMessage = "O campo é obrigatótio.")]
        public uint IdPropriedade { get; set; }

    }
}
