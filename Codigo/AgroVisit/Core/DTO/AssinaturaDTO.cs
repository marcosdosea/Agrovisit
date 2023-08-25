namespace Core.DTO
{
    public class AssinaturaDTO
    {
        public int Id { get; set; }

        public float Valor { get; set; }

        public string Status { get; set; } = null!;

        public DateTime Data { get; set; }

        public DateTime? DataCancelamento { get; set; }

        public int IdPlano { get; set; }

        public int IdEngenheiroAgronomo { get; set; }

        public virtual EngenheiroagronomoDTO IdEngenheiroAgronomoNavigation { get; set; } = null!;

        public virtual PlanoDTO IdPlanoNavigation { get; set; } = null!;
    }
}