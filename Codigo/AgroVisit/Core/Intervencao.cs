using System.ComponentModel.DataAnnotations;

namespace Core;

public partial class Intervencao
{
    public uint Id { get; set; }

    public string Pratica { get; set; } = null!;

    public string? Descricao { get; set; }

    [Display(Name = "Data de Aplicação")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime? DataAplicacao { get; set; }

    public string? TipoProduto { get; set; }

    public float? AreaTratada { get; set; }

    public string Status { get; set; } = null!;

    public uint IdProjeto { get; set; }

    public virtual Projeto IdProjetoNavigation { get; set; } = null!;
}
