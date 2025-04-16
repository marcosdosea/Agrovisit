using Core;
using System.ComponentModel.DataAnnotations;

namespace AgroVisitWeb.Models
{
    public class ClienteViewModel
    {

        [Required]
        [Key]
        public uint Id { get; set; }

        [Display(Name = "CPF")]
        [StringLength(15)]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Cpf { get; set; } = null!;

        [Display(Name = "Nome")]
        [StringLength(50)]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Nome { get; set; } = null!;

        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date)]
        public DateTime? DataNascimento { get; set; }

        [Display(Name = "Cidade")]
        [StringLength(20)]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Cidade { get; set; } = null!;

        [Display(Name = "Bairro")]
        [StringLength(25)]
        public string? Bairro { get; set; }

        [Display(Name = "Estado")]
        [StringLength(2)]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Estado { get; set; } = null!;

        [Display(Name = "Rua")]
        [StringLength(60)]
        public string? Rua { get; set; }

        [Display(Name = "N° Residência")]
        public int? NumeroCasa { get; set; }

        [Display(Name = "Telefone")]
        [StringLength(15)]
        [RegularExpression(@"^\((1[1-9]|2[12478]|3[1-8]|4[1-9]|5[1-5]|6[1-9]|7[134579]|8[1-9]|9[1-9])\) 9\d{4}-\d{4}$", ErrorMessage = "Número inválido")]
        public string? Telefone { get; set; }

        [Display(Name = "Agrônomo")]
        [Required]
        public uint IdEngenheiroAgronomo { get; set; }

        public IEnumerable<Propriedade>? ListaPropriedade { get; set; }
    }
}