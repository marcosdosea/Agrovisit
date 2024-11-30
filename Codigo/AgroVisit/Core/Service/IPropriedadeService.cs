namespace Core.Service
{
    public interface IPropriedadeService
    {
        public uint Create(Propriedade propriedade);
        public void Edit(Propriedade propriedade);
        public void Delete(uint id);
        public Propriedade Get(uint id);
        public IEnumerable<Propriedade> GetAll();
        public IEnumerable<Propriedade> GetByCliente(uint idCliente);
        public IEnumerable<Propriedade> GetByNome(string nome);
    }
}
