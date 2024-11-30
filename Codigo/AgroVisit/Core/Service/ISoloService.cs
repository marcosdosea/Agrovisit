namespace Core.Service
{
    public interface ISoloService
    {
        public Solo? Get(uint id);
        public IEnumerable<Solo> GetAll();
    }
}
