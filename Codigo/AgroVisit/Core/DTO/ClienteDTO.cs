namespace Core.DTO
{
    public partial class ClienteDTO
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

        public virtual EngenheiroagronomoDTO IdEngenheiroAgronomoNavigation { get; set; } = null!;

        public virtual ICollection<PropriedadeDTO> Propriedades { get; set; } = new List<PropriedadeDTO>();
    }

}