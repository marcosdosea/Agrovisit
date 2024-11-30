using Core.DTO;

namespace Core.Service
{
    public interface IEngenheiroAgronomoService
    {
        public int Create(Engenheiroagronomo engenheiroAgronomo);
        public void Edit(Engenheiroagronomo engenheiroAgronomo);
        public void Delete(int id);
        public Engenheiroagronomo Get(int id);
        public IEnumerable<Engenheiroagronomo> GetAll();
        public IEnumerable<EngenheiroagronomoDTO> GetByNome(string nome);
    }
}
