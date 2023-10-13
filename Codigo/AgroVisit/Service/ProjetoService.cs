using Core;
using Core.Datatables;
using Core.DTO;
using Core.Service;
using Microsoft.EntityFrameworkCore;
using System.Windows.Markup;

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
            if (projeto != null) 
            {
                _context.Remove(projeto);
                _context.SaveChanges();
            } 
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
        public Projeto? Get(uint id)
        {
            return _context.Projetos.Find(id);
        }
        /// <summary>
        /// Obter projetos pelo nome
        /// </summary>
        /// <param name="nome"></param>
        /// <returns> Projetos pelo nome </returns>
        public IEnumerable<Projeto> GetByNome(string nome)
        {
            return _context.Projetos.Where(
                projeto => projeto.Nome.StartsWith(nome)).AsNoTracking();
        }
        /// <summary>
        /// Obter todos os projetos
        /// </summary>
        /// <returns> Todos os projetos </returns>
        /// 

        public IEnumerable<Projeto> GetAll()
        {
            return _context.Projetos.AsNoTracking();
        }
        /// <summary>
        /// Obter todos os projetosDto
        /// </summary>
        /// <returns> todos os projetosDto</returns>
        public IEnumerable<ProjetoDto> GetAllDto()
        {
            var query = from projetos in _context.Projetos
                        select new ProjetoDto
                        {
                            Id = projetos.Id,
                            Nome = projetos.Nome,
                            DataInicio = projetos.DataInicio,
                            Status = projetos.Status,
                            NomeCliente = projetos.IdPropriedadeNavigation.IdClienteNavigation.Nome,
                            NomePropriedade = projetos.IdPropriedadeNavigation.Nome
                        };
            
            return query.AsNoTracking();
        }
        public ProjetoAllDto GetDetailsDeleteAll(uint id)
        {
            var query = from projetos in _context.Projetos
                        where projetos.Id == id
                        select new ProjetoAllDto
                        {
                            Id = projetos.Id,
                            Nome = projetos.Nome,
                            DataInicio = projetos.DataInicio,
                            Status = projetos.Status,
                            NomeCliente = projetos.IdPropriedadeNavigation.IdClienteNavigation.Nome,
                            NomePropriedade = projetos.IdPropriedadeNavigation.Nome,
                            DataPrevista = projetos.DataPrevista,
                            DataConclusao = projetos.DataConclusao,
                            Descricao = projetos.Descricao,
                            Anexo = projetos.Anexo,
                            NumeroVisita = projetos.NumeroVisita,
                            Valor = projetos.Valor,
                            QuantParcela = projetos.QuantParcela,
                            Intervencoes = projetos.Intervencoes
                        };

            return (ProjetoAllDto)query.AsNoTracking();
        }
        /// <summary>
        /// Obter projetos pela data
        /// </summary>
        /// <param name="data"></param>
        /// <returns> Projetos ordenados pela data prevista </returns>
        public IEnumerable<Projeto> GetByData(DateTime data)
        {
            var query = from projeto in _context.Projetos
                        where projeto.DataPrevista.Equals(data)
                        orderby projeto.DataPrevista
                        select projeto;
            return query.AsNoTracking();
        }
        /// <summary>
        /// Obter projetos pelo status
        /// </summary>
        /// <param name="status"></param>
        /// <returns> Projeto de acordo com seu status </returns>
        public IEnumerable<Projeto> GetByStatus(string status)
        {
            return _context.Projetos.Where(
               projeto => projeto.Status.StartsWith(status)).AsNoTracking();
        }
        /// <summary>
        /// Obter as intervenções de um projeto
        /// </summary>
        /// <returns> As intervenções de um projeto </returns>
        public IEnumerable<Intervencao> GetAllIntervencoes(uint id)
        {
            var query = from intervencao in _context.Intervencoes
                        where intervencao.IdProjeto == id
                        select intervencao;
            return query.AsNoTracking();
        }
        /// <summary>
        /// Obter as contas de um projeto
        /// </summary>
        /// <returns> As contas de um projeto </returns>
        public IEnumerable<Conta> GetAllConta(uint id)
        {
            var query = from conta in _context.Contas
                        where conta.IdProjeto == id
                        select conta;
            return query.AsNoTracking();
        }
        /// <summary>
        /// Obter os projetos de uma propriedade
        /// </summary>
        /// <param name="idPropriedade"></param>
        /// <returns> Projetos de uma propriedade </returns>
        public IEnumerable<Projeto> GetByPropriedade(uint idPropriedade)
        {
            var query = from projeto in _context.Projetos
                        where projeto.IdPropriedade == idPropriedade
                        select projeto;
            return query.AsNoTracking();
        }

        /// <summary>
        /// Retorna uma página de dados
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public DatatableResponse<ProjetoDto> GetDataPage(DatatableRequest request)
        {
            var projetos =  from projeto in _context.Projetos
                            select new ProjetoDto
                            {
                                Id = projeto.Id,
                                Nome = projeto.Nome, 
                                DataInicio = projeto.DataInicio,
                                Status = projeto.Status,
                                NomeCliente = projeto.IdPropriedadeNavigation.IdClienteNavigation.Nome,
                                NomePropriedade = projeto.IdPropriedadeNavigation.Nome,
                                Valor = projeto.Valor
                            };
            // total de registros na tabela
            var totalRecords = projetos.Count();

            // filtra pelo campos de busca
            if (request.Search != null && request.Search.GetValueOrDefault("value") != null)
            {
                projetos = projetos.Where(projeto => projeto.Id.ToString().Contains(request.Search.GetValueOrDefault("value"))
                                              || projeto.Nome.ToLower().Contains(request.Search.GetValueOrDefault("value")));
            }

            // ordenação pelas colunas permitidas
            if (request.Order != null && request.Order[0].GetValueOrDefault("column").Equals("0"))
            {
                if (request.Order[0].GetValueOrDefault("dir").Equals("asc"))
                    projetos = projetos.OrderBy(projeto => projeto.Nome);
                else
                    projetos = projetos.OrderByDescending(projeto => projeto.Nome);
            }
            else if (request.Order != null && request.Order[0].GetValueOrDefault("column").Equals("3"))
            {
                if (request.Order[0].GetValueOrDefault("dir").Equals("asc"))
                    projetos = projetos.OrderBy(projeto => projeto.DataInicio);
                else
                    projetos = projetos.OrderByDescending(projeto => projeto.DataInicio);
            }
            

            // total de registros filtrados
            int countRecordsFiltered = projetos.Count();
            // paginação que será exibida
            projetos = projetos.Skip(request.Start).Take(request.Length);
            return new DatatableResponse<ProjetoDto>()
            {
                Data = projetos.ToList(),
                Draw = request.Draw,
                RecordsFiltered = countRecordsFiltered,
                RecordsTotal = totalRecords
            };
        }
    }
}