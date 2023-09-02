using Core;
using Core.DTO;
using Core.Service;

namespace Service
{
    /// <summary>
    /// Manter dados dos clientes no banco de dados
    /// </summary>
    public class ClienteService : IClienteService
    {
        private readonly AgroVisitContext _context;

        public ClienteService(AgroVisitContext context) {
            _context = context;
        }

        /// <summary>
        /// Insere cliente na base de dados
        /// </summary>
        /// <param name="cliente"></param>
        /// <returns>Id cliente</returns>
        public uint Create(Cliente cliente)
        {
            _context.Add(cliente);
            _context.SaveChanges();
            return cliente.Id;
        }

        /// <summary>
        /// Remove cliente da base de dados
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            var cliente = _context.Clientes.Find(id);
            _context.Clientes.Remove(cliente);
            _context.SaveChanges();
        }

        /// <summary>
        /// Altera cliente na base de dados
        /// </summary>
        /// <param name="cliente"></param>
        public void Edit(Cliente cliente)
        {
            _context.Update(cliente);
            _context.SaveChanges();
        }

        /// <summary>
        /// Obtém cliente da base de dados
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Cliente</returns>
        public Cliente Get(int id)
        {
            return _context.Clientes.Find(id);
        }

        /// <summary>
        /// Obtém todos os clientes da base de dados
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Cliente> GetAll()
        {
            return _context.Clientes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nome"></param>
        /// <returns>Cliente</returns>
        public IEnumerable<ClienteDTO> GetByNome(string nome)
        {
            return (IEnumerable<ClienteDTO>)_context.Clientes.Where(
                cliente => cliente.Nome.StartsWith(nome));
        }
    }
}
