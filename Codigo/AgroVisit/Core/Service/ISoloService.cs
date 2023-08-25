using Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service
{
    public interface ISoloService
    {
        public int Create(Solo solo);
        public void Edit(Solo solo);
        public void Delete(int id);
        public Solo Get(int id);
        public IEnumerable<Solo> GetAll();
        public IEnumerable<SoloDTO> GetByNome(string nome);
    }
}
