using Core.DTO;

namespace Core.Service
{
    public interface IAssinaturaService
    {
        public int Create(Assinatura assinatura);
        public void Edit(Assinatura assinatura);
        public void Delete(int id);
        public Assinatura Get(int id);
        public IEnumerable<Assinatura> GetAll();
        public IEnumerable<AssinaturaDTO> GetAllByPlano(string plano);
        public IEnumerable<AssinaturaDTO> GetAllByStatus(string status);
    }
}
