namespace Core.DTO
{
    public partial class PlanoDTO
    {
        public uint Id { get; set; }

        public string Nome { get; set; } = null!;

        public float Valor { get; set; }

        public virtual ICollection<AssinaturaDTO> Assinaturas { get; set; } = new List<AssinaturaDTO>();
    }
}
