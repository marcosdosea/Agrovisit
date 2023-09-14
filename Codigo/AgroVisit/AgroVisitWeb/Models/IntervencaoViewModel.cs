using Core;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AgroVisitWeb.Models
{
    public class IntervencaoViewModel
    {
        [Key]
        public uint Id { get; set; }

        [Display(Name = "Pratica")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Pratica { get; set; } = null!;
        
        [Display(Name = "Descrição")]
        [StringLength(1000)]
        public string? Descricao { get; set; }

        [Display(Name = "Data Aplicacao")]
        public DateTime? DataAplicacao { get; set; }

        [Display(Name = "TipoProduto")]
        public String? TipoProduto { get; set; }

        [Display(Name = "AreaTratada")]
        public float? AreaTratada { get; set; }
        
        [Display(Name = "Status")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Status { get; set; } = null!;
        
        [Display(Name = "IdProjeto")]
        public uint IdProjeto { get; set; }
    }
}

    

