using Core.DTO;


namespace Core.Service
{
    public interface IVisitaService
    {
        public uint Create(Visita visita);
        public void Edit(Visita visita);
        public void Delete(uint id);
        public Visita Get(uint id);
        public IEnumerable<Visita> GetAll();
        public IEnumerable<VisitaDTO> GetAllByDate(DateTime data);
        public IEnumerable<Visita> GetAllByStatus(String status);
        public IEnumerable<Visita> GetByPropriedade(uint idPropriedade);
    }
}
