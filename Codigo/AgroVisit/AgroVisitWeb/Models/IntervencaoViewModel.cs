using Core;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AgroVisitWeb.Models
{
    public class IntervencaoViewModel
    {
        [Key]
        public uint Id { get; set; }

        public string Pratica { get; set; } = null!;
        [Display(Name = "Pratica")]
        [Required(ErrorMessage = "Campo obrigatório")]

        public string? Descricao { get; set; }
        [Display(Name = "Descrição")]
        [StringLength(1000)]

        public DateTime? DataAplicacao { get; set; }

        public String? TipoProduto { get; set; }

        public float? AreaTratada { get; set; }

        public string Status { get; set; } = null!;
        [Display(Name = "Propriedade")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public uint IdProjeto { get; set; }
        [Display(Name = "Propriedade")]

    }
}

    

