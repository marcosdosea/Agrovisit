using Core;
using Core.Datatables;
using Core.DTO;
using Core.Service;
using Microsoft.EntityFrameworkCore;

namespace Service
{
    /// <summary>
    /// Manter dados dos clientes no banco de dados
    /// </summary>
    public class ClienteService : IClienteService
    {
        private readonly AgroVisitContext _context;

        public ClienteService(AgroVisitContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Insere cliente na base de dados
        /// </summary>
        /// <param name="cliente"></param>
        /// <returns>Id do cliente cliente criado</returns>
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
        public void Delete(uint id)
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
        public Cliente? Get(uint id)
        {
            return _context.Clientes.Find(id);
        }

        /// <summary>
        /// Obtém todos os clientes da base de dados
        /// </summary>
        /// <returns>Todos os clientes</returns>
        public IEnumerable<Cliente> GetAll()
        {
            return _context.Clientes.AsNoTracking();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nome"></param>
        /// <returns>Clientes retornados pelo nome</returns>
        public IEnumerable<Cliente> GetByNome(string nome)
        {
            return (IEnumerable<Cliente>)_context.Clientes.Where(
                cliente => cliente.Nome.StartsWith(nome)).AsNoTracking();
        }

        /// <summary>
        /// Retorna uma página de dados
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public DatatableResponse<Cliente> GetDataPage(DatatableRequest request)
        {
            var clientes = _context.Clientes.AsNoTracking();
            // total de registros na tabela
            var totalRecords = clientes.Count();

            // filtra pelo campos de busca
            if (request.Search != null && request.Search.GetValueOrDefault("value") != null)
            {
                clientes = clientes.Where(cliente => cliente.Id.ToString().Contains(request.Search.GetValueOrDefault("value"))
                                              || cliente.Nome.ToLower().Contains(request.Search.GetValueOrDefault("value"))
                                              || cliente.Cidade.ToLower().Contains(request.Search.GetValueOrDefault("value")));
            }

            // ordenação pelas colunas permitidas
            if (request.Order != null && request.Order[0].GetValueOrDefault("column").Equals("0"))
            {
                if (request.Order[0].GetValueOrDefault("dir").Equals("asc"))
                    clientes = clientes.OrderBy(cliente => cliente.Nome);
                else
                    clientes = clientes.OrderByDescending(cliente => cliente.Nome);
            }
            else if (request.Order != null && request.Order[0].GetValueOrDefault("column").Equals("3"))
            {
                if (request.Order[0].GetValueOrDefault("dir").Equals("asc"))
                    clientes = clientes.OrderBy(cliente => cliente.Cidade);
                else
                    clientes = clientes.OrderByDescending(cliente => cliente.Cidade);
            }

            // total de registros filtrados
            int countRecordsFiltered = clientes.Count();
            // paginação que será exibida
            clientes = clientes.Skip(request.Start).Take(request.Length);
            return new DatatableResponse<Cliente>()
            {
                Data = clientes.ToList(),
                Draw = request.Draw,
                RecordsFiltered = countRecordsFiltered,
                RecordsTotal = totalRecords
            };
        }
    }
}
