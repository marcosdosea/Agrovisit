using Core.Datatables;

namespace Core.Service
{
    public interface IClienteService
    {
        Task<uint> Create(Cliente cliente);
        Task Edit(Cliente cliente);
        Task Delete(uint id);
        Task<Cliente?> Get(uint id);
        Task<IEnumerable<Cliente>> GetAll();
        Task<IEnumerable<Cliente>> GetByNome(string nome);
        Task<DatatableResponse<Cliente>> GetDataPage(DatatableRequest request);
    }
}