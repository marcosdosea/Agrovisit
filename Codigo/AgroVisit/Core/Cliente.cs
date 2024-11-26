namespace Core;

public partial class Cliente
{
    public uint Id { get; set; }

    public string Cpf { get; set; } = null!;

    public string Nome { get; set; } = null!;

    public DateTime? DataNascimento { get; set; }

    public string Cidade { get; set; } = null!;

    public string? Bairro { get; set; }

    public string Estado { get; set; } = null!;

    public string? Rua { get; set; }

    public int? NumeroCasa { get; set; }

    public string? Telefone { get; set; }

    public uint IdEngenheiroAgronomo { get; set; }

    public virtual Engenheiroagronomo IdEngenheiroAgronomoNavigation { get; set; } = null!;

    public virtual ICollection<Propriedade> Propriedades { get; set; } = new List<Propriedade>();
}
