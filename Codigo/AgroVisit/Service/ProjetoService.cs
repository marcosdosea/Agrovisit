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
        public void Delete(int id)
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
        /// <param name="id">id</param>
        public Projeto Get(int id)
        {
            return _context.Projetos.Find(id);
        }
        /// <summary>
        /// Obter dados de todos os projetos
        /// </summary>
        /// <returns></returns>
        /// 
        public IEnumerable<Projeto> GetByNome(string nome)
        {
            return _context.Projetos.Where(
                projeto => projeto.Nome.StartsWith(nome)).AsNoTracking();
        }
        public IEnumerable<Projeto> GetAll()
        {
            return _context.Projetos.AsNoTracking();
        }
        /// <summary>
        /// Obter projetos pela data
        /// </summary>
        /// <param name="data"></param>
        /// <returns> projeto </returns>
        public IEnumerable<ProjetoDTO> GetByData(DateTime data)
        {
            var query = from projeto in _context.Projetos
                        where projeto.DataPrevista == data
                        orderby projeto.DataPrevista
                        select projeto;
            return (IEnumerable<ProjetoDTO>)query.AsNoTracking();
        }
        /// <summary>
        /// Obter projetos pelo status
        /// </summary>
        /// <param name="status"></param>
        /// <returns> projeto </returns>
        public IEnumerable<ProjetoDTO> GetByStatus(string status)
        {
            return (IEnumerable<ProjetoDTO>)_context.Projetos.Where(
               projeto => projeto.Status.StartsWith(status)).AsNoTracking();
        }
        
        public IEnumerable<Intervencao> GetAllIntervencaos()
        {
            return _context.Intervencaos;
        }

        public IEnumerable<Conta> GetAllConta()
        {
            return _context.Conta;
        }
    }
}