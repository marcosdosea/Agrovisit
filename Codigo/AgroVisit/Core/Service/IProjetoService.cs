using Core.Datatables;
using Core.DTO;

namespace Core.Service
{
    public interface IProjetoService
    {
        Task<uint> Create(Projeto projeto);
        Task Edit(Projeto projeto);
        Task Delete(uint id);
        Task<Projeto?> Get(uint id);
        Task<IEnumerable<Projeto>> GetAll();
        Task<IEnumerable<ProjetoDto>> GetAllDto();
        Task<ProjetoAllDto?> GetDetailsDeleteAll(uint id);
        Task<IEnumerable<Projeto>> GetByNome(string nome);
        Task<IEnumerable<Projeto>> GetByData(DateTime data);
        Task<IEnumerable<Projeto>> GetByStatus(string status);
        Task<IEnumerable<Projeto>> GetByPropriedade(uint idPropriedade);
        Task<IEnumerable<Intervencao>> GetAllIntervencoes(uint idPropriedade);
        Task<IEnumerable<Conta>> GetAllContas(uint idPropriedade);
        Task<DatatableResponse<ProjetoDto>> GetDataPage(DatatableRequest request);
    }
}