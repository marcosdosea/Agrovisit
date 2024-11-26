using System.ComponentModel.DataAnnotations;

namespace Core.DTO
{
    public partial class ProjetoDto
    {
        public uint Id { get; set; }

        public string? Nome { get; set; }

        [Display(Name = "Data de inicio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{dd/mm/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataInicio { get; set; }

        public string Status { get; set; } = null!;

        [Display(Name = "Propriedade")]
        public string? NomePropriedade { get; set; }

        [Display(Name = "Cliente")]
        public string? NomeCliente { get; set; }

        [Display(Name = "Valor")]
        public float Valor { get; set; }

    }
}