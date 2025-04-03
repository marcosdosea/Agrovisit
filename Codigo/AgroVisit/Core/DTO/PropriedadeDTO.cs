using System.ComponentModel.DataAnnotations;

namespace Core.DTO
{
    public partial class PropriedadeDto
    {
        public uint Id { get; set; }

        public string Nome { get; set; } = null!;
        
        [Display(Name = "Cliente")]
        public string? NomeCliente { get; set; }
        public string Estado { get; set; } = null!;

        public string Cidade { get; set; } = null!;

        
    }
}
