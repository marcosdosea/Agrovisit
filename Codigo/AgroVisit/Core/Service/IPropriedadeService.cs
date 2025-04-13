using Core.DTO;

namespace Core.Service
{
    public interface IPropriedadeService
    {
        Task<uint> Create(Propriedade propriedade);
        Task Edit(Propriedade propriedade);
        Task Delete(uint id);
        Task<Propriedade?> Get(uint id);
        Task<IEnumerable<Propriedade>> GetAll();
        Task<IEnumerable<PropriedadeDto>> GetAllDto();
        Task<IEnumerable<Propriedade>> GetByCliente(uint idCliente);
        Task<IEnumerable<Propriedade>> GetByNome(string nome);
    }
}
