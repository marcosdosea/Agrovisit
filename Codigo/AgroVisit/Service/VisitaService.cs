using Core;
using Core.Datatables;
using Core.Service;
using Microsoft.EntityFrameworkCore;

namespace Service
{
    public class VisitaService : IVisitaService
    {
        private readonly AgroVisitContext _context;

        public VisitaService(AgroVisitContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Cria uma visita no banco de dados
        /// </summary>
        /// <param name="visita">Dados da visita</param>
        /// <returns>Id da visita cadastrada</returns>
        public async Task<uint> Create(Visita visita)
        {
            _context.Add(visita);
            await _context.SaveChangesAsync();
            return visita.Id;
        }

        /// <summary>
        /// Remove visita da base de dados
        /// </summary>
        /// <param name="id"></param>
        public async Task Delete(uint id)
        {
            var visita = await _context.Visita.FindAsync(id);
            if (visita == null) return;

            _context.Remove(visita);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Altera visita na base de dados
        /// </summary>
        /// <param name="visita"></param>
        public async Task Edit(Visita visita)
        {
            _context.Update(visita);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Obtém visita da base de dados
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Visita</returns>
        public async Task<Visita> Get(uint id)
        {
            return await _context.Visita.FindAsync(id);
        }

        /// <summary>
        /// Obtém todas as visitas da base de dados
        /// </summary>
        /// <returns>Dados de todas as visitas</returns>
        public async Task<IEnumerable<Visita>> GetAll()
        {
            return await _context.Visita
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Obtém todas as visitas de uma data
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Todas as visitas de uma data</returns>
        public async Task<IEnumerable<Visita>> GetAllByDate(DateTime data)
        {
            return await _context.Visita
                .Where(v => v.DataHora == data)
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Obtém as visitas pelo status
        /// </summary>
        /// <param name="status"></param>
        /// <returns>Todas as visitas para um status</returns>
        public async Task<IEnumerable<Visita>> GetAllByStatus(string status)
        {
            return await _context.Visita
                .Where(v => v.Status == status)
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Obtém todas as visitas de uma propriedade
        /// </summary>
        /// <param name="idPropriedade"></param>
        /// <returns>Todas as visitas de uma datpropriedade</returns>
        public async Task<IEnumerable<Visita>> GetByPropriedade(uint idPropriedade)
        {
            return await _context.Visita
                .Where(v => v.IdPropriedade == idPropriedade)
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Retorna uma página de dados
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<DatatableResponse<Visita>> GetDataPage(DatatableRequest request)
        {
            var visitas = _context.Visita
                .AsNoTracking()
                .Include(v => v.IdPropriedadeNavigation) // Garante que Nome esteja carregado
                .AsQueryable();

            // Total de registros na tabela (antes de aplicar filtro)
            var totalRecords = await visitas.CountAsync();

            // Filtro de busca
            if (!string.IsNullOrEmpty(request.Search?.GetValueOrDefault("value")))
            {
                var searchValue = request.Search.GetValueOrDefault("value").ToLower();

                visitas = visitas.Where(visita =>
                    visita.Id.ToString().Contains(searchValue)
                    || visita.IdPropriedadeNavigation.Nome.ToLower().Contains(searchValue)
                    || visita.Status.ToLower().Contains(searchValue)
                );
            }

            // Ordenação
            if (request.Order != null)
            {
                var coluna = request.Order[0].GetValueOrDefault("column");
                var direcao = request.Order[0].GetValueOrDefault("dir");

                if (coluna == "0")
                {
                    visitas = direcao == "asc"
                        ? visitas.OrderBy(v => v.Status)
                        : visitas.OrderByDescending(v => v.Status);
                }
                else if (coluna == "3")
                {
                    visitas = direcao == "asc"
                        ? visitas.OrderBy(v => v.IdPropriedadeNavigation.Nome)
                        : visitas.OrderByDescending(v => v.IdPropriedadeNavigation.Nome);
                }
            }

            // Total após filtros
            var countRecordsFiltered = await visitas.CountAsync();

            // Paginação
            if (request.Length != -1)
            {
                visitas = visitas.Skip(request.Start).Take(request.Length);
            }

            // Resultado final
            var listaVisitas = await visitas.ToListAsync();

            return new DatatableResponse<Visita>
            {
                Data = listaVisitas,
                Draw = request.Draw,
                RecordsFiltered = countRecordsFiltered,
                RecordsTotal = totalRecords
            };
        }
    }
}
