using Core;
using Core.Datatables;
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
        public async Task<uint> Create(Projeto projeto)
        {
            await _context.AddAsync(projeto);
            await _context.SaveChangesAsync();
            return projeto.Id;
        }

        /// <summary>
        /// Excluir projeto da base de dados
        /// </summary>
        /// <param name="id">id projeto excluir</param>
        public async Task Delete(uint id)
        {
            var projeto = await _context.Projetos.FindAsync(id);
            if (projeto != null)
            {
                _context.Remove(projeto);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Editar projeto na base de dados
        /// </summary>
        /// <param name="projeto"></param>
        public async Task Edit(Projeto projeto)
        {
            _context.Update(projeto);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Obter dados de um projeto
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Dados de um projeto </returns>
        public async Task<Projeto?> Get(uint id)
        {
            return await _context.Projetos.FindAsync(id);
        }

        /// <summary>
        /// Obter projetos pelo nome
        /// </summary>
        /// <param name="nome"></param>
        /// <returns> Projetos pelo nome </returns>
        public async Task<IEnumerable<Projeto>> GetByNome(string nome)
        {
            return await _context.Projetos
                .Where(p => p.Nome.StartsWith(nome))
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Obter todos os projetos
        /// </summary>
        /// <returns> Todos os projetos </returns>
        /// 

        public async Task<IEnumerable<Projeto>> GetAll()
        {
            return await _context.Projetos.AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// Obter todos os projetosDto
        /// </summary>
        /// <returns> todos os projetosDto</returns>
        public async Task<IEnumerable<ProjetoDto>> GetAllDto()
        {
            return await _context.Projetos
                .Select(p => new ProjetoDto
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    DataInicio = p.DataInicio,
                    Status = p.Status,
                    NomeCliente = p.IdPropriedadeNavigation.IdClienteNavigation.Nome,
                    NomePropriedade = p.IdPropriedadeNavigation.Nome,
                    Valor = p.Valor
                })
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ProjetoAllDto?> GetDetailsDeleteAll(uint id)
        {
            return await _context.Projetos
                .Where(p => p.Id == id)
                .Select(p => new ProjetoAllDto
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    DataInicio = p.DataInicio,
                    Status = p.Status,
                    NomeCliente = p.IdPropriedadeNavigation.IdClienteNavigation.Nome,
                    NomePropriedade = p.IdPropriedadeNavigation.Nome,
                    DataPrevista = p.DataPrevista,
                    DataConclusao = p.DataConclusao,
                    Descricao = p.Descricao,
                    Anexo = p.Anexo,
                    NumeroVisita = p.NumeroVisita,
                    Valor = p.Valor,
                    QuantParcela = p.QuantParcela,
                    Intervencoes = p.Intervencoes
                })
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Obter projetos pela data
        /// </summary>
        /// <param name="data"></param>
        /// <returns> Projetos ordenados pela data prevista </returns>
        public async Task<IEnumerable<Projeto>> GetByData(DateTime data)
        {
            return await _context.Projetos
                .Where(p => p.DataPrevista == data)
                .OrderBy(p => p.DataPrevista)
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Obter projetos pelo status
        /// </summary>
        /// <param name="status"></param>
        /// <returns> Projeto de acordo com seu status </returns>
        public async Task<IEnumerable<Projeto>> GetByStatus(string status)
        {
            return await _context.Projetos
                .Where(p => p.Status.StartsWith(status))
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Obter as intervenções de um projeto
        /// </summary>
        /// <returns> As intervenções de um projeto </returns>
        public async Task<IEnumerable<Intervencao>> GetAllIntervencoes(uint id)
        {
            return await _context.Intervencoes
                .Where(i => i.IdProjeto == id)
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Obter as contas de um projeto
        /// </summary>
        /// <returns> As contas de um projeto </returns>
        public async Task<IEnumerable<Conta>> GetAllContas(uint id)
        {
            return await _context.Conta
                .Where(c => c.IdProjeto == id)
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Obter os projetos de uma propriedade
        /// </summary>
        /// <param name="idPropriedade"></param>
        /// <returns> Projetos de uma propriedade </returns>
        public async Task<IEnumerable<Projeto>> GetByPropriedade(uint idPropriedade)
        {
            return await _context.Projetos
                .Where(p => p.IdPropriedade == idPropriedade)
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Retorna uma página de dados
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<DatatableResponse<ProjetoDto>> GetDataPage(DatatableRequest request)
        {
            var query = _context.Projetos.Select(projeto => new ProjetoDto
            {
                Id = projeto.Id,
                Nome = projeto.Nome,
                DataInicio = projeto.DataInicio,
                Status = projeto.Status,
                NomeCliente = projeto.IdPropriedadeNavigation.IdClienteNavigation.Nome,
                NomePropriedade = projeto.IdPropriedadeNavigation.Nome,
                Valor = projeto.Valor
            });

            int totalRecords = await query.CountAsync();

            if (request.Search?.GetValueOrDefault("value") is string searchValue && !string.IsNullOrWhiteSpace(searchValue))
            {
                query = query.Where(projeto =>
                    projeto.Id.ToString().Contains(searchValue) ||
                    projeto.Nome.ToLower().Contains(searchValue.ToLower()));
            }

            int countRecordsFiltered = await query.CountAsync();

            if (request.Order is not null && request.Order[0].TryGetValue("column", out string? column) && request.Order[0].TryGetValue("dir", out string? dir))
            {
                switch (column)
                {
                    case "0":
                        query = dir == "asc" ? query.OrderBy(p => p.Nome) : query.OrderByDescending(p => p.Nome);
                        break;
                    case "3":
                        query = dir == "asc" ? query.OrderBy(p => p.DataInicio) : query.OrderByDescending(p => p.DataInicio);
                        break;
                }
            }

            if (request.Length != -1)
                query = query.Skip(request.Start).Take(request.Length);

            return new DatatableResponse<ProjetoDto>
            {
                Data = await query.ToListAsync(),
                Draw = request.Draw,
                RecordsFiltered = countRecordsFiltered,
                RecordsTotal = totalRecords
            };
        }
    }
}