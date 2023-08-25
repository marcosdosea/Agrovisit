using Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service
{
    public interface IVisitaService
    {
        public int Create(Visita visita);
        public void Edit(Visita visita);
        public void Delete(int id);
        public Visita Get(int id);
        public IEnumerable<Visita> GetAll();
        public IEnumerable<VisitaDTO> GetByNome(string nome);
    }
}
