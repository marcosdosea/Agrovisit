using Core.DTO;

namespace Core.Service
{
    public interface IPropriedadeService
    {
        public uint Create(Propriedade propriedade);
        public void Edit(Propriedade propriedade);
        public void Delete(int id);
        public Propriedade Get(int id);
        public IEnumerable<Propriedade> GetAll();
        public IEnumerable<Propriedade> GetByCliente(string cliente);
        public IEnumerable<Propriedade> GetByNome(string nome);
    }
}
