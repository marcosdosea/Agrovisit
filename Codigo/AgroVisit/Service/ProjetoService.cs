using Core;
using Core.DTO;
using Core.Service;
using Microsoft.EntityFrameworkCore;

namespace Service
{
    /// <summary>
    /// Manter dados do projetos no banco de dados
    /// </summary>
    public class ProjetoService : IProjetoService
    {
        private readonly AgroVisitContext _context;

        public ProjetoService(AgroVisitContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Inserir projeto na base de dados
        /// </summary>
        /// <param name="projeto">dados do projeto</param>
        /// <returns>id</returns>
        public uint Create(Projeto projeto)
        {
            _context.Add(projeto);
            _context.SaveChanges();
            return projeto.Id;
        }
        /// <summary>
        /// Excluir projeto da base de dados
        /// </summary>
        /// <param name="id">id projeto excluir</param>
        public void Delete(uint id)
        {
            var projeto = _context.Projetos.Find(id);
            _context.Remove(projeto);
            _context.SaveChanges();
        }
        /// <summary>
        /// Editar projeto na base de dados
        /// </summary>
        /// <param name="projeto"></param>
        public void Edit(Projeto projeto)
        {
            _context.Update(projeto);
            _context.SaveChanges();
        }
        /// <summary>
        /// Obter dados de um projeto
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Dados de um projeto </returns>
        public Projeto Get(uint id)
        {
            return _context.Projetos.Find(id);
        }
        /// <summary>
        /// Obter projetos pelo nome
        /// </summary>
        /// <param name="nome"></param>
        /// <returns> Projetos pelo nome </returns>
        public IEnumerable<ProjetoDTO> GetByNome(string nome)
        {
            return (IEnumerable<ProjetoDTO>)_context.Projetos.Where(
                projeto => projeto.Nome.StartsWith(nome)).AsNoTracking();
        }
        /// <summary>
        /// Obter todos os projetos
        /// </summary>
        /// <returns> Todos os projetos </returns>
        public IEnumerable<Projeto> GetAll()
        {
            return _context.Projetos.AsNoTracking();
        }
        /// <summary>
        /// Obter projetos pela data
        /// </summary>
        /// <param name="data"></param>
        /// <returns> Projetos ordenados pela data prevista </returns>
        public IEnumerable<ProjetoDTO> GetByData(DateTime data)
        {
            var query = from projeto in _context.Projetos
                        where projeto.DataPrevista.Equals(data)
                        orderby projeto.DataPrevista
                        select projeto;
            return (IEnumerable<ProjetoDTO>)query.AsNoTracking();
        }
        /// <summary>
        /// Obter projetos pelo status
        /// </summary>
        /// <param name="status"></param>
        /// <returns> Projeto de acordo com seu status </returns>
        public IEnumerable<ProjetoDTO> GetByStatus(string status)
        {
            return (IEnumerable<ProjetoDTO>)_context.Projetos.Where(
               projeto => projeto.Status.StartsWith(status)).AsNoTracking();
        }
        /// <summary>
        /// Obter as intervenções de um projeto
        /// </summary>
        /// <returns> As intervenções de um projeto </returns>
        public IEnumerable<IntervencaoDTO> GetAllIntervencoes(uint idPropriedade)
        {
            var query = from projeto in _context.Projetos
                        join intervencao in _context.Projetos on projeto.Id equals intervencao.IdPropriedade
                        select intervencao;
            return (IEnumerable<IntervencaoDTO>)query.AsNoTracking().ToList();
        }
        /// <summary>
        /// Obter as contas de um projeto
        /// </summary>
        /// <returns> As contas de um projeto </returns>
        public IEnumerable<ContaDTO> GetAllConta(uint idPropriedade)
        {
            var query = from projeto in _context.Projetos
                        join conta in _context.Projetos on projeto.Id equals conta.IdPropriedade
                        select conta;
            return (IEnumerable<ContaDTO>)query.AsNoTracking().ToList();
        }
        /// <summary>
        /// Obter os projetos de uma propriedade
        /// </summary>
        /// <param name="idPropriedade"></param>
        /// <returns> Projetos de uma propriedade </returns>
        public IEnumerable<Projeto> GetByPropriedade(uint idPropriedade)
        {
            var query = from propriedade in _context.Propriedades
                        join projeto in _context.Projetos on propriedade.Id equals projeto.IdPropriedade
                        select projeto;
            return query.AsNoTracking().ToList();
        }
    }
}