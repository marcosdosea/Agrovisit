using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AgroVisitWeb.Models
{
    public class IntervencaoViewModel
    {
        [Key]
        public uint Id { get; set; }

        [Display(Name = "Pr�tica")]
        [Required(ErrorMessage = "Campo obrigat�rio")]
        public string Pratica { get; set; } = null!;

        [Display(Name = "Descri��o")]
        [StringLength(1000)]
        public string? Descricao { get; set; }

        [Display(Name = "Data Aplica��o")]
        [Required(ErrorMessage = "Campo obrigat�rio")]
        public DateTime? DataAplicacao { get; set; }

        [Display(Name = "Produto utilizado")]
        public String? TipoProduto { get; set; }

        [Display(Name = "Area Tratada")]
        public float? AreaTratada { get; set; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "Campo obrigat�rio")]
        public string Status { get; set; } = null!;

        [Display(Name = "Projeto")]
        public int IdProjeto { get; set; }
    }
}



