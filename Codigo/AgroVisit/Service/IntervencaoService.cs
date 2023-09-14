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
        /// Inserir Intervencao na base de dados
        /// </summary>
        /// <param name="intervencao">dados da intervencao</param>
        /// <returns>id</returns>

        public uint Create(Intervencao intervencao)
        {
            _context.Add(intervencao);
            _context.SaveChanges();
            return intervencao.Id;
        }
        /// <summary>
        /// Excluir Intervencao da base de dados
        /// </summary>
        /// <param name="id">id Intervencao excluir</param>

        public void Delete(int id)
        {

            var intervencao = _context.Intervencoes.Find(id);
            _context.Remove(intervencao);
            _context.SaveChanges();
        }

        /// <summary>
        /// Editar Intervencao na base de dados
        /// </summary>
        /// <param name="Intervencao"></param>
        public void Edit(Intervencao intervencao)
        {
            _context.Update(intervencao);
            _context.SaveChanges();
        }
        /// <summary>
        /// Obter dados de uma Intervencao
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Dados de uma Intervencao </returns>

        public Intervencao Get(int id)
        {
            return _context.Intervencoes.Find(id);
        }

        /// <summary>
        /// Obter todas as Intervencoes
        /// </summary>
        /// <returns> Todos as intervencoes </returns>
        public IEnumerable<Intervencao> GetAll()
        {
            return _context.Intervencoes.AsNoTracking();
        }

        /// <summary>
        /// Obter Intervencoes pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Intervencoes pelo nome </returns>
        public IEnumerable<Intervencao> GetByProjeto(uint IdProjeto)
        {
            var query = from Intervencao in _context.Intervencoes
                where Intervencao.IdProjeto.Equals(IdProjeto)
                select Intervencao;
            return query.AsNoTracking();

        }

    }
}
