namespace Core;

public partial class Assinatura
{
    public uint Id { get; set; }

    public float Valor { get; set; }

    public string Status { get; set; } = null!;

    public DateTime Data { get; set; }

    public DateTime? DataCancelamento { get; set; }

    public uint IdPlano { get; set; }

    public uint IdEngenheiroAgronomo { get; set; }

    public virtual Engenheiroagronomo IdEngenheiroAgronomoNavigation { get; set; } = null!;

    public virtual Plano IdPlanoNavigation { get; set; } = null!;
}
