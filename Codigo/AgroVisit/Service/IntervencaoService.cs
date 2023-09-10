using Core;
using Core.DTO;
using Core.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// Inserir Intervenção na base de dados
        /// </summary>
        /// <param name="Intervencao">dados da Intervencao</param>
        /// <returns>id</returns>

        public uint Create(Intervencao intervencao)
        {
            _context.Add(intervencao);
            _context.SaveChanges();
            return intervencao.Id;
        }
        /// <summary>
        /// Criar uma nova Intervencao
        /// </summary>
        /// <param name="Intervencao"> dados da intervencao</param>
        /// <returns> id da Intervencao<>/returns>

        public void Delete(int id)
        {

            var intervencao = _context.Intervencoes.Find(id);
            _context.Remove(intervencao);
            _context.SaveChanges();
        }

        /// <summary>
        /// Excluir Intervencao da base de dados
        /// </summary>
        /// <param name="id">id intervencao excluir</param>
        public void Edit(Intervencao intervencao)
        {
            _context.Update(intervencao);
            _context.SaveChanges();
        }
        /// <summary>
        /// Editar Intervencao na base de dados
        /// </summary>
        /// <param name="Intervencao"></param>

        public Intervencao Get(int id)
        {
            return _context.Intervencoes.Find(id);
        }

        public IEnumerable<Intervencao> GetAll()
        {
            return _context.Intervencoes.AsNoTracking();
        }

        public IEnumerable<Intervencao> GetByProjeto(int IdProjeto)
        {
            var query = from Intervencao in _context.Intervencoes
                where Intervencao.IdProjeto.Equals(IdProjeto)
                select Intervencao;
            return query.AsNoTracking();

        }

    }
}
