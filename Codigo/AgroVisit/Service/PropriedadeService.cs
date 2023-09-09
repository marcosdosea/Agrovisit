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
        public uint Create(Propriedade propriedade)
        {
            _context.Add(propriedade);
            _context.SaveChanges();
            return propriedade.Id;
        }

        /// <summary>
        /// Remove propriedade do banco de dados
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            var _propriedade = _context.Propriedades.Find(id);
            _context.Remove(_propriedade);
            _context.SaveChanges();
        }

        /// <summary>
        /// Altera propriedade do banco de dados
        /// </summary>
        /// <param name="propriedade"></param>
        public void Edit(Propriedade propriedade)
        {
            _context.Update(propriedade);
            _context.SaveChanges();
        }

        /// <summary>
        /// Obtém propriedade do banco de dado
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Propriedade </returns>
        public Propriedade Get(int id)
        {
            return _context.Propriedades.Find(id);
        }

        /// <summary>
        /// Obtém todas as propriedades do banco de dados
        /// </summary>
        /// <returns> Todas as propriedades </returns>
        public IEnumerable<Propriedade> GetAll()
        {
            return _context.Propriedades.AsNoTracking();
        }

        /// <summary>
        /// Obtém propriedades de um cliente
        /// </summary>
        /// <param name="cliente"></param>
        /// <returns> Propriedades do cliente </returns>
        public IEnumerable<Propriedade> GetByCliente(int idCliente)
        {
            var query = from Propriedade in _context.Propriedades
                        where Propriedade.IdCliente.Equals(idCliente)
                        select Propriedade;

            return query.AsNoTracking();
        }

        /// <summary>
        /// Obtém propriedade por nome
        /// </summary>
        /// <param name="nome"></param>
        /// <returns> Propriedades </returns>
        public IEnumerable<Propriedade> GetByNome(string nome)
        {
            var query = from Propriedade in _context.Propriedades
                        where Propriedade.Nome.Equals(nome)
                        select Propriedade;

            return query.AsNoTracking();
        }
    }
}
