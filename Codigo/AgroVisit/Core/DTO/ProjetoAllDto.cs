using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Core.DTO
{
    public class ProjetoAllDto
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

        public DateTime DataPrevista { get; set; }

        public string? Descricao { get; set; }

        public byte[]? Anexo { get; set; }

        public DateTime? DataConclusao { get; set; }

        public int? NumeroVisita { get; set; }

        public uint QuantParcela { get; set; }

        [Display(Name = "Propriedade:")]
        public SelectList? ListaPropriedades { get; set; }

        public IEnumerable<Intervencao>? Intervencoes { get; set; }


    }
}
