using Core;
using System.ComponentModel.DataAnnotations;

namespace AgroVisitWeb.Models
{
    public class PlanoViewModel
    {
        [Key]
        [Required]
        public uint Id { get; set; }

        [Display(Name = "CPF")]
        [StringLength(50)]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Nome { get; set; } = null!;

        [Display(Name = "Valor")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public float Valor { get; set; }

        [Display(Name = "Assinaturas")]
        public virtual ICollection<Assinatura> Assinaturas { get; set; } = new List<Assinatura>();
    }
}
