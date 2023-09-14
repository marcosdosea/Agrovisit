using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core;

public partial class PropriedadeViewModel
{
    [Key]
    public uint Id { get; set; }
    [Required(ErrorMessage = "O campo é obrigatótio.")]
    public string Nome { get; set; } = null!;
    [Required(ErrorMessage = "O campo é obrigatótio.")]
    public string Estado { get; set; } = null!;
    [Required(ErrorMessage = "O campo é obrigatótio.")]
    public string Cidade { get; set; } = null!;
    [Display(Name = "Quantidade de funcionários")]
    public int? QuantFuncionario { get; set; }
    [Display(Name = "Área de reserva legal")]
    public float? AreaReserva { get; set; }
    [Display(Name = "Área de preservação")]
    public float? AreaPreservar { get; set; }
    [Display(Name = "Cadastro Ambiental Rural(CAR)")]
    public string? Car { get; set; }
    [Display(Name = "Certificado de Cadastro de Imóvel Rural(CCIR)")]
    public string? Ccir { get; set; }
    [Display(Name = "Imposto Territoria Rural(ITR)")]
    public string? Itr { get; set; }
    [Display(Name = "Georreferenciamento")]
    public byte[]? Georreferenciamento { get; set; }
    [Display(Name = "Matrícula do imóvel")]
    public byte[]? MatriculaImovel { get; set; }
    [Display(Name = "Número de animais")]
    public int? NumAnimais { get; set; }
    [Display(Name = "Raça")]
    public string? Raca { get; set; }
    [Display(Name = "Fonte de alimento")]
    public string? FonteAlimento { get; set; }
    [Display(Name = "Área de pasto")]
    public float? AreaPasto { get; set; }
    [Display(Name = "Histórico da produção")]
    public byte[]? HistoricoProducao { get; set; }
    [Display(Name = "Área total")]
    public float? AreaTotal { get; set; }
    [Display(Name = "Área cultivada")]
    public float? AreaCultivada { get; set; }
    [Display(Name = "Comércio ou armazenamento")]
    public string? Comercializacao { get; set; }
    [Display(Name = "Histórico produção agrícola")]
    public byte[]? HistoricoProdAgricola { get; set; }
    [Display(Name = "Histórico de fitossanidade")]
    public byte[]? HistoricoFitossanidade { get; set; }
    [Display(Name = "Solo")]
    public uint IdSolo { get; set; }
    [Display(Name = "Cultura")]
    public uint IdCultura { get; set; }
    [Display(Name = "Cliente")]
    public uint IdCliente { get; set; }
    [Display(Name = "Engenheiro agrônomo")]
    public uint IdEngenheiroAgronomo { get; set; }
    public IEnumerable<Projeto>? ListaProjetos { get; set; }

    public IEnumerable<Visita>? ListaVisitas { get; set; }
    public SelectList? ListaClientes { get; set; }
}