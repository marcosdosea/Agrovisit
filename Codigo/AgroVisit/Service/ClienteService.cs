using Core;
using Core.Datatables;
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
        public async Task<uint> Create(Cliente cliente)
        {
            _context.Add(cliente);
            await _context.SaveChangesAsync();
            return cliente.Id;
        }

        /// <summary>
        /// Remove cliente da base de dados
        /// </summary>
        /// <param name="id"></param>
        public async Task Delete(uint id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Altera cliente na base de dados
        /// </summary>
        /// <param name="cliente"></param>
        public async Task Edit(Cliente cliente)
        {
            _context.Update(cliente);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Obtém cliente da base de dados
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Cliente</returns>
        public async Task<Cliente?> Get(uint id)
        {
            return await _context.Clientes.FindAsync(id);
        }

        /// <summary>
        /// Obtém todos os clientes da base de dados
        /// </summary>
        /// <returns>Todos os clientes</returns>
        public async Task<IEnumerable<Cliente>> GetAll()
        {
            return await _context.Clientes.AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nome"></param>
        /// <returns>Clientes retornados pelo nome</returns>
        public async Task<IEnumerable<Cliente>> GetByNome(string nome)
        {
            return await _context.Clientes
                .Where(cliente => cliente.Nome.StartsWith(nome))
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Retorna uma página de dados
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<DatatableResponse<Cliente>> GetDataPage(DatatableRequest request)
        {
            var clientes = _context.Clientes.AsNoTracking();

            var totalRecords = await clientes.CountAsync();

            if (request.Search != null && request.Search.GetValueOrDefault("value") != null)
            {
                var searchValue = request.Search.GetValueOrDefault("value")!.ToLower();
                clientes = clientes.Where(cliente =>
                    cliente.Id.ToString().Contains(searchValue) ||
                    cliente.Nome.ToLower().Contains(searchValue) ||
                    cliente.Cidade.ToLower().Contains(searchValue));
            }

            if (request.Order != null && request.Order[0].GetValueOrDefault("column").Equals("0"))
            {
                clientes = request.Order[0].GetValueOrDefault("dir").Equals("asc")
                    ? clientes.OrderBy(cliente => cliente.Nome)
                    : clientes.OrderByDescending(cliente => cliente.Nome);
            }
            else if (request.Order != null && request.Order[0].GetValueOrDefault("column").Equals("3"))
            {
                clientes = request.Order[0].GetValueOrDefault("dir").Equals("asc")
                    ? clientes.OrderBy(cliente => cliente.Cidade)
                    : clientes.OrderByDescending(cliente => cliente.Cidade);
            }

            int countRecordsFiltered = await clientes.CountAsync();

            if (request.Length != -1)
            {
                clientes = clientes.Skip(request.Start).Take(request.Length);
            }

            return new DatatableResponse<Cliente>()
            {
                Data = await clientes.ToListAsync(),
                Draw = request.Draw,
                RecordsFiltered = countRecordsFiltered,
                RecordsTotal = totalRecords
            };
        }
    }
}
