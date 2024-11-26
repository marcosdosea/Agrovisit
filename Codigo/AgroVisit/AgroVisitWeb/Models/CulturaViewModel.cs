using System.ComponentModel.DataAnnotations;

namespace AgroVisitWeb.Models
{
    public class CulturaViewModel
    {
        [Key]
        public uint Id { get; set; }
        public string Nome { get; set; } = null!;
    }
}
