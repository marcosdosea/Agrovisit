using Core;
using Core.Service;
using Microsoft.EntityFrameworkCore;

namespace Service
{
    public class SoloService : ISoloService
    {
        private readonly AgroVisitContext _context;

        public SoloService(AgroVisitContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Obtém solo da base de dados
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Solo</returns>
        public Solo? Get(uint id)
        {
            return _context.Solos.Find(id);
        }
        /// <summary>
        /// Obtém todas os solos da base de dados
        /// </summary>
        /// <returns>Dados de todos os solos</returns>
        public IEnumerable<Solo> GetAll()
        {
            return _context.Solos.AsNoTracking();
        }
    }
}
