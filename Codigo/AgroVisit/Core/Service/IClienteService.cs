using Core.DTO;

namespace Core.Service
{
    public interface IClienteService
    {
        public uint Create(Cliente cliente);
        public void Edit(Cliente cliente);
        public void Delete(uint id);
        public Cliente Get(uint id);
        public IEnumerable<Cliente> GetAll();
        public IEnumerable<ClienteDTO> GetByNome(string nome);
    }
}
