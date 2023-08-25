using Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service
{
    public interface IProjetoService
    {
        public int Create(Projeto projeto);
        public void Edit(Projeto projeto);
        public void Delete(int id);
        public Projeto Get(int id);
        public IEnumerable<Projeto> GetAll();
        public IEnumerable<ProjetoDTO> GetByNome(string nome);
        public IEnumerable<ProjetoDTO> GetByData(DateTime data);
        public IEnumerable<ProjetoDTO> GetByStatus(string status);
    }
}
