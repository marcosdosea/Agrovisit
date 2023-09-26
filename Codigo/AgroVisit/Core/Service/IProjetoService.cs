using Core.Datatables;
using Core.DTO;

namespace Core.Service
{
    public interface IProjetoService
    {
        public uint Create(Projeto projeto);
        public void Edit(Projeto projeto);
        public void Delete(uint id);
        public Projeto? Get(uint id);
        public IEnumerable<Projeto> GetAll();
        public IEnumerable<ProjetoDTO> GetAllDto();
        /*public IQueryable<ProjetoAllDto> GetDetailsDeleteAll(uint id);*/
        public IEnumerable<Projeto> GetByNome(string nome);
        public IEnumerable<Projeto> GetByData(DateTime data);
        public IEnumerable<Projeto> GetByStatus(string status);
        public IEnumerable<Projeto> GetByPropriedade(uint idPropriedade);
        public IEnumerable<Intervencao> GetAllIntervencoes(uint idPropriedade);
        public IEnumerable<Conta> GetAllConta(uint idPropriedade);
        DatatableResponse<ProjetoDTO> GetDataPage(DatatableRequest request);
    }
}