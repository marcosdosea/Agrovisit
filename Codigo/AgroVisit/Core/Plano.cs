using System;
using System.Collections.Generic;

namespace Core;

public partial class Plano
{
    public uint Id { get; set; }

    public string Nome { get; set; } = null!;

    public float Valor { get; set; }

    public virtual ICollection<Assinatura> Assinaturas { get; set; } = new List<Assinatura>();
}
