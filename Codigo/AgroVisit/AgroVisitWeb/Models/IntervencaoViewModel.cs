using System.ComponentModel.DataAnnotations;

namespace AgroVisitWeb.Models
{
    public class IntervencaoViewModel
    {
        [Required]
        [Key]
        public uint Id { get; set; }

        [Display(Name = "Prática")]
        [StringLength(500)]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Pratica { get; set; } = null!;

        [Display(Name = "Descrição")]
        [StringLength(500)]
        public string? Descricao { get; set; }

        [Display(Name = "Data Aplicação")]
        [Required(ErrorMessage = "Campo obrigatório")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? DataAplicacao { get; set; }

        [Display(Name = "Produto utilizado")]
        [StringLength(150)]
        public String? TipoProduto { get; set; }

        [Display(Name = "Área Tratada")]
        public float? AreaTratada { get; set; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Status { get; set; } = null!;

        [Required]
        [Display(Name = "Projeto")]
        public int IdProjeto { get; set; }
    }
}



