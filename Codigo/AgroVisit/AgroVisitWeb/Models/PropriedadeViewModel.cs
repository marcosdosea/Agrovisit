﻿using Core;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AgroVisitWeb.Models
{

    public class PropriedadeViewModel
    {
        [Required]
        [Key]
        public uint Id { get; set; }

        [Display(Name = "Nome da propriedade")]
        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(50)]
        public string Nome { get; set; } = null!;

        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(2)]
        public string Estado { get; set; } = null!;

        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(50)]
        public string Cidade { get; set; } = null!;

        [Display(Name = "Quantidade de funcionários")]
        public int? QuantFuncionario { get; set; }

        [Display(Name = "Área de reserva legal")]
        public float? AreaReserva { get; set; }

        [Display(Name = "Área de preservação")]
        public float? AreaPreservar { get; set; }

        [StringLength(50)]
        [Display(Name = "CAR")]
        public string? Car { get; set; }

        [StringLength(50)]
        [Display(Name = "CCIR")]
        public string? Ccir { get; set; }

        [StringLength(50)]
        [Display(Name = "ITR")]
        public string? Itr { get; set; }

        [Display(Name = "Georreferenciamento")]
        public byte[]? Georreferenciamento { get; set; }

        [Display(Name = "Matrícula do imóvel")]
        public byte[]? MatriculaImovel { get; set; }

        [Display(Name = "Área total")]
        public float? AreaTotal { get; set; }

        [Display(Name = "Área cultivada")]
        public float? AreaCultivada { get; set; }

        [Display(Name = "Cultivo para:")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string? Comercializacao { get; set; } = null!;

        [Display(Name = "Histórico produção agrícola")]
        public byte[]? HistoricoProdAgricola { get; set; }

        [Display(Name = "Histórico de fitossanidade")]
        public byte[]? HistoricoFitossanidade { get; set; }

        [Required]
        [Display(Name = "Solo")]
        public uint IdSolo { get; set; }

        [Required]
        [Display(Name = "Cultura")]
        public uint IdCultura { get; set; }

        [Display(Name = "Cliente")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public uint IdCliente { get; set; }

        [Required]
        [Display(Name = "Engenheiro agrônomo")]
        public uint IdEngenheiroAgronomo { get; set; }

        public IEnumerable<Projeto>? ListaProjetos { get; set; }

        public IEnumerable<Visita>? ListaVisitas { get; set; }

        public SelectList? ListaClientes { get; set; }

        public SelectList? ListaCulturas { get; set; }

        public SelectList? ListaSolos { get; set; }

        [Display(Name = "Solo")]
        public string? NomeSolo { get; set; }

        [Display(Name = "Cultura")]
        public string? NomeCultura { get; set; }

        [Display(Name = "Cliente")]
        public string? NomeCliente { get; set; }
    }
}