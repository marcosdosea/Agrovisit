using Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service
{
    public interface IPropriedadeService
    {
        public int Create(Propriedade propriedade);
        public void Edit(Propriedade propriedade);
        public void Delete(int id);
        public Propriedade Get(int id);
        public IEnumerable<Propriedade> GetAll();
        public IEnumerable<PropriedadeDTO> GetByNome(string nome);

    }
}
