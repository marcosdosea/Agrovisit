﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AgroVisitAPI.Models
{
    public class IntervencaoViewModel
    {
        [Key]
        public uint Id { get; set; }

        [Display(Name = "Prática")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Pratica { get; set; } = null!;

        [Display(Name = "Descrição")]
        [StringLength(1000)]
        public string? Descricao { get; set; }

        [Display(Name = "Data Aplicação")]
        public DateTime? DataAplicacao { get; set; }

        [Display(Name = "Tipo de Produto utilizado")]
        public String? TipoProduto { get; set; }

        [Display(Name = "Area Tratada")]
        public float? AreaTratada { get; set; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Status { get; set; } = null!;

        [Display(Name = "Projeto")]
        public uint IdProjeto { get; set; }
        public SelectList? ListaProjetos { get; set; }
    }
}
