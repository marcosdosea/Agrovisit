namespace Core.Service
{
    public interface IIntervencaoService
    {
        Task<uint> Create(Intervencao intervencao);
        Task Edit(Intervencao intervencao);
        Task Delete(uint id);
        Task<Intervencao?> Get(uint id);
        Task<IEnumerable<Intervencao>> GetAll();
        Task<IEnumerable<Intervencao>> GetByProjeto(uint idProjeto);
    }
}