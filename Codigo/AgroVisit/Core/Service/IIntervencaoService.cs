namespace Core.Service
{
    public interface IIntervencaoService
    {
        public uint Create(Intervencao intervencao);
        public void Edit(Intervencao intervencao);
        public void Delete(uint id);
        public Intervencao Get(uint id);
        public IEnumerable<Intervencao> GetAll();
        public IEnumerable<Intervencao> GetByProjeto(uint idProjeto);


    }


}
