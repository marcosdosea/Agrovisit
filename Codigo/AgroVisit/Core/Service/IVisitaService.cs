using Core.Datatables;


namespace Core.Service
{
    public interface IVisitaService
    {
        Task<uint> Create(Visita visita);
        Task Edit(Visita visita);
        Task Delete(uint id);
        Task<Visita> Get(uint id);
        Task<IEnumerable<Visita>> GetAll();
        Task<IEnumerable<Visita>> GetAllByDate(DateTime data);
        Task<IEnumerable<Visita>> GetAllByStatus(string status);
        Task<IEnumerable<Visita>> GetByPropriedade(uint idPropriedade);
        Task<DatatableResponse<Visita>> GetDataPage(DatatableRequest request);
    }
}
