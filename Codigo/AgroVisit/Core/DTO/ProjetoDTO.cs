namespace Core.DTO
{
    public partial class ProjetoDTO
    {
        public uint Id { get; set; }

        public string? Nome { get; set; }

        public float Valor { get; set; }

        public uint QuantParcela { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime DataPrevista { get; set; }

        public string? Descricao { get; set; }

        public byte[]? Anexo { get; set; }

        public string Status { get; set; } = null!;

        public uint IdPropriedade { get; set; }

    }
}