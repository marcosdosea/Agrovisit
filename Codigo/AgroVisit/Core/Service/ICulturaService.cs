using Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service
{
    public interface ICulturaService
    {
        public int Create(Cultura cultura);
        public void Edit(Cultura cutura);
        public void Delete(int id);
        public Cultura Get(int id);
        public IEnumerable<Cultura> GetAll();
        public IEnumerable<CulturaDTO> GetByNome(string nome);
    }
}
