using Core;
using Core.Service;
using Microsoft.EntityFrameworkCore;

namespace Service
{
    public class CulturaService : ICulturaService
    {
        private readonly AgroVisitContext _context;

        public CulturaService(AgroVisitContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Obtém cultura da base de dados
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Cultura</returns>
        public Cultura? Get(uint id)
        {
            return _context.Culturas.Find(id);
        }
        /// <summary>
        /// Obtém todas as culturas da base de dados
        /// </summary>
        /// <returns>Dados de todas as culturas</returns>
        public IEnumerable<Cultura> GetAll()
        {
            return _context.Culturas.AsNoTracking();
        }
    }
}
