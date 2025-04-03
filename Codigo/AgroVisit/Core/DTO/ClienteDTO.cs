namespace Core.DTO
{
    public partial class ClienteDTO
    {
        public uint Id { get; set; }

        public string Cpf { get; set; } = null!;

        public string Nome { get; set; } = null!;

        public string Cidade { get; set; } = null!;

        public string? Bairro { get; set; }

        public string Estado { get; set; } = null!;

        public string? Rua { get; set; }

        public int? NumeroCasa { get; set; }

        public string? Telefone { get; set; }

        public virtual ICollection<PropriedadeDto> Propriedades { get; set; } = new List<PropriedadeDto>();
    }

}