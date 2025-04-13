using Core;
using Core.DTO;
using Core.Service;
using Microsoft.EntityFrameworkCore;

namespace Service
{
    /// <summary>
    /// Manter dados da propriedade no banco de dados
    /// </summary>
    public class PropriedadeService : IPropriedadeService
    {
        private readonly AgroVisitContext _context;

        public PropriedadeService(AgroVisitContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Insere Propriedade no banco de dados
        /// </summary>
        /// <param name="propriedade"></param>
        /// <returns> Id da propriedade </returns>
        public async Task<uint> Create(Propriedade propriedade)
        {
            await _context.AddAsync(propriedade);
            await _context.SaveChangesAsync();
            return propriedade.Id;
        }

        /// <summary>
        /// Remove propriedade do banco de dados
        /// </summary>
        /// <param name="id"></param>
        public async Task Delete(uint id)
        {
            var _propriedade = await _context.Propriedades.FindAsync(id);
            if (_propriedade != null)
            {
                _context.Remove(_propriedade);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Altera propriedade do banco de dados
        /// </summary>
        /// <param name="propriedade"></param>
        public async Task Edit(Propriedade propriedade)
        {
            _context.Update(propriedade);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Obtém propriedade do banco de dado
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Propriedade </returns>
        public async Task<Propriedade?> Get(uint id)
        {
            return await _context.Propriedades.FindAsync(id);
        }

        /// <summary>
        /// Obtém todas as propriedades do banco de dados
        /// </summary>
        /// <returns> Todas as propriedades </returns>
        public async Task<IEnumerable<Propriedade>> GetAll()
        {
            return await _context.Propriedades
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Obtém propriedades de um cliente
        /// </summary>
        /// <param name="cliente"></param>
        /// <returns> Propriedades do cliente </returns>
        public async Task<IEnumerable<Propriedade>> GetByCliente(uint idCliente)
        {
            return await _context.Propriedades
                .Where(p => p.IdCliente == idCliente)
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Obtém propriedade por nome
        /// </summary>
        /// <param name="nome"></param>
        /// <returns> Propriedades </returns>
        public async Task<IEnumerable<Propriedade>> GetByNome(string nome)
        {
            return await _context.Propriedades
                .Where(p => p.Nome == nome)
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Obter todos os PropriedadeDto
        /// </summary>
        /// <returns> todos os PropriedadeDto</returns>
        public async Task<IEnumerable<PropriedadeDto>> GetAllDto()
        {
            return await _context.Propriedades
                .Select(propriedade => new PropriedadeDto
                {
                    Id = propriedade.Id,
                    Nome = propriedade.Nome,
                    NomeCliente = propriedade.IdClienteNavigation.Nome,
                    Cidade = propriedade.Cidade,
                    Estado = propriedade.Estado
                })
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
