namespace Core.Service
{
    public interface IPlanoService
    {
        Task<uint> Create(Plano plano);
        Task Edit(Plano plano);
        Task Delete(int id);
        Task<Plano> Get(int id);
        Task<IEnumerable<Plano>> GetAll();
    }
}
