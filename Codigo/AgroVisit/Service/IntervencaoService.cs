using Core;
using Core.Service;
using Microsoft.EntityFrameworkCore;

namespace Service
{
    /// <summary>
    /// Manter dados das Intervencoes no banco de dados
    /// </summary>
    public class IntervencaoService : IIntervencaoService
    {
        private readonly AgroVisitContext _context;

        public IntervencaoService(AgroVisitContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Inserir Intervencao na base de dados
        /// </summary>
        /// <param name="intervencao">dados da intervencao</param>
        /// <returns>id</returns>
        public async Task<uint> Create(Intervencao intervencao)
        {
            _context.Add(intervencao);
            await _context.SaveChangesAsync();
            return intervencao.Id;
        }

        /// <summary>
        /// Excluir Intervencao da base de dados
        /// </summary>
        /// <param name="id">id Intervencao excluir</param>
        public async Task Delete(uint id)
        {
            var intervencao = await _context.Intervencoes.FindAsync(id);
            if (intervencao != null)
            {
                _context.Remove(intervencao);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Editar Intervencao na base de dados
        /// </summary>
        /// <param name="Intervencao"></param>
        public async Task Edit(Intervencao intervencao)
        {
            _context.Update(intervencao);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Obter dados de uma Intervencao
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Dados de uma Intervencao </returns>
        public async Task<Intervencao?> Get(uint id)
        {
            return await _context.Intervencoes.FindAsync(id);
        }

        /// <summary>
        /// Obter todas as Intervencoes
        /// </summary>
        /// <returns> Todos as intervencoes </returns>
        public async Task<IEnumerable<Intervencao>> GetAll()
        {
            return await _context.Intervencoes
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Obter Intervencoes pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Intervencoes pelo nome </returns>
        public async Task<IEnumerable<Intervencao>> GetByProjeto(uint idProjeto)
        {
            return await _context.Intervencoes
                .Where(i => i.IdProjeto == idProjeto)
                .AsNoTracking()
                .ToListAsync();
        }

    }
}
