namespace Core;

public partial class Cultura
{
    public uint Id { get; set; }

    public string Nome { get; set; } = null!;

    public virtual ICollection<Propriedade> Propriedades { get; set; } = new List<Propriedade>();
}
