﻿using System.ComponentModel.DataAnnotations;

namespace AgroVisitWeb.Controllers
{
    public class ClienteViewModel
    {
        [Display(Name = "ID Cliente")]
        public uint Id { get; set; }

        [Display(Name = "CPF")]
        [StringLength(15)]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Cpf { get; set; } = null!;

        [Display(Name = "Nome")]
        [StringLength(50)]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Nome { get; set; } = null!;

        [Display(Name = "Data de nascimento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
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
        [RegularExpression(@"^(\([1-9]{2}\)|[1-9]{2}) ?(?:[2-8]|9[1-9])[0-9]{3}\-?[0-9]{4}$", ErrorMessage = "Número inválido")]
        public string? Telefone { get; set; }
    }
}