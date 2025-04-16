using System.ComponentModel.DataAnnotations;

namespace AgroVisitWeb.Models
{
    public class SoloViewModel
    {
        [Key]
        public uint Id { get; set; }

        [StringLength(50)]
        public string Nome { get; set; } = null!;
    }
}
