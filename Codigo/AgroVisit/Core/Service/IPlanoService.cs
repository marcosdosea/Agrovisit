namespace Core.Service
{
    public interface IPlanoService
    {
        Task<uint> Create(Plano plano);
        Task Edit(Plano plano);
        Task Delete(uint id);
        Task<Plano> Get(uint id);
        Task<IEnumerable<Plano>> GetAll();
    }
}
