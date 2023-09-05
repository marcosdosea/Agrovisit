using Core.DTO;

namespace Core.Service
{
    public interface IClienteService
    {
        public uint Create(Cliente cliente);
        public void Edit(Cliente cliente);
        public void Delete(int id);
        public Cliente Get(int id);
        public IEnumerable<Cliente> GetAll();
        public IEnumerable<ClienteDTO> GetByNome(string nome);
    }
}
