using System;
using System.Collections.Generic;

namespace Core;

public partial class Engenheiroagronomo
{
    public uint Id { get; set; }

    public string Nome { get; set; } = null!;

    public string Cpf { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Senha { get; set; } = null!;

    public string? Celular { get; set; }

    public virtual ICollection<Assinatura> Assinaturas { get; set; } = new List<Assinatura>();

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    public virtual ICollection<Propriedade> Propriedades { get; set; } = new List<Propriedade>();
}
