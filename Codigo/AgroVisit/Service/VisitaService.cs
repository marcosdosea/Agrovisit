using Core;
using Core.DTO;
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
        public uint Create(Visita visita)
        {
            _context.Add(visita);
            _context.SaveChanges();
            return visita.Id;
        }
        /// <summary>
        /// Remove visita da base de dados
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            var visita = _context.Visita.Find(id);
            if (visita == null) return;

            _context.Remove(visita);
            _context.SaveChanges();
        }
        /// <summary>
        /// Altera visita na base de dados
        /// </summary>
        /// <param name="visita"></param>
        public void Edit(Visita visita)
        {
            _context.Update(visita);
            _context.SaveChanges();
        }
        /// <summary>
        /// Obtém visita da base de dados
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Cliente</returns>
        public Visita Get(int id)
        {
            return _context.Visita.Find(id);
        }
        /// <summary>
        /// Obtém todas as visitas da base de dados
        /// </summary>
        /// <returns>Dados de todas as visitas</returns>
        public IEnumerable<Visita> GetAll()
        {
            return _context.Visita.AsNoTracking();
        }
        /// <summary>
        /// Obtém todas as visitas de uma data
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Todas as visitas de uma data</returns>
        public IEnumerable<VisitaDTO> GetAllByDate(DateTime data)
        {
            var query = from VisitaDTO in _context.Visita
                        where VisitaDTO.DataHora.Equals(data)
                        select new VisitaDTO
                        {
                            Id = VisitaDTO.Id,
                            Observacoes = VisitaDTO.Observacoes,
                            DataHora = VisitaDTO.DataHora,
                            Status = VisitaDTO.Status
                        };

            return query.AsNoTracking();
        }

        public IEnumerable<Visita> GetAllByStatus(string status)
        {
            var query = from Visita in _context.Visita
                        where Visita.Status.Equals(status)
                        select Visita;

            return query.AsNoTracking();
        }
    }
}
