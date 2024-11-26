namespace Core.Service
{
    public interface ICulturaService
    {
        public Cultura? Get(uint id);
        public IEnumerable<Cultura> GetAll();
    }
}
