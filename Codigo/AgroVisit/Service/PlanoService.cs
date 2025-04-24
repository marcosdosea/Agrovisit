using Core;
using Core.Service;
using Microsoft.EntityFrameworkCore;

namespace Service
{
    internal class PlanoService : IPlanoService
    {
        private readonly AgroVisitContext _context;

        /// <summary>
        /// Insere plano na base de dados
        /// </summary>
        /// <param name="plano"></param>
        /// <returns>Id do plano criado</returns>
        public async Task<uint> Create(Plano plano)
        {
            _context.Add(plano);
            await _context.SaveChangesAsync();
            return plano.Id;
        }

        /// <summary>
        /// Remove plano da base de dados
        /// </summary>
        /// <param name="id"></param>
        public async Task Delete(int id)
        {
            var plano = await _context.Planos.FindAsync(id);
            if (plano != null)
            {
                _context.Planos.Remove(plano);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Altera plano na base de dados
        /// </summary>
        /// <param name="plano"></param>
        public async Task Edit(Plano plano)
        {
            _context.Update(plano);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Obtém plano da base de dados
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Plano</returns>
        public async Task<Plano> Get(int id)
        {
            return await _context.Planos.FindAsync(id);
        }

        /// <summary>
        /// Obtém todos os planos da base de dados
        /// </summary>
        /// <returns>Todos os planos</returns>
        public async Task<IEnumerable<Plano>> GetAll()
        {
            return await _context.Planos.AsNoTracking().ToListAsync();
        }
    }
}
