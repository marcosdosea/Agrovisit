namespace Core.DTO
{
    public partial class EngenheiroagronomoDTO
    {
        public uint Id { get; set; }

        public string Nome { get; set; } = null!;

        public string Cpf { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Senha { get; set; } = null!;

        public string? Celular { get; set; }

        public virtual ICollection<Assinatura> Assinaturas { get; set; } = new List<Assinatura>();

        public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

        public virtual ICollection<PropriedadeDto> Propriedades { get; set; } = new List<PropriedadeDto>();
    }
}
