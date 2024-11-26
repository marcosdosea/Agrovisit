namespace Core.Service
{
    public interface IPlanoService
    {
        public int Create(Plano plano);
        public void Edit(Plano plano);
        public void Delete(int id);
        public Plano Get(int id);
        public IEnumerable<Plano> GetAll();
    }
}
