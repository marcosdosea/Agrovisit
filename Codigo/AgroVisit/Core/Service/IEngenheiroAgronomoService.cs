using Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service
{
    public interface IEngenheiroAgronomoService
    {
        public int Create(Engenheiroagronomo engenheiroAgronomo);
        public void Edit(Engenheiroagronomo engenheiroAgronomo);
        public void Delete(int id);
        public Engenheiroagronomo Get(int id);
        public IEnumerable<Engenheiroagronomo> GetAll();
        public IEnumerable<EngenheiroagronomoDTO> GetByNome(string nome);
    }
}
