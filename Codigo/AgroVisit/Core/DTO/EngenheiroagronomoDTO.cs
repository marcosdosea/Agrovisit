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

        public virtual ICollection<AssinaturaDTO> Assinaturas { get; set; } = new List<AssinaturaDTO>();

        public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

        public virtual ICollection<PropriedadeDTO> Propriedades { get; set; } = new List<PropriedadeDTO>();
    }
}
