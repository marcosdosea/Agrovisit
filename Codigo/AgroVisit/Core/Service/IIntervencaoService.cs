using Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service
{
    public interface IIntervencaoService
    {
        public uint Create(Intervencao intervencao);
        public void Edit(Intervencao intervencao);
        public void Delete(int id);
        public Intervencao Get(int id);
        public IEnumerable<Intervencao> GetAll();
        public IEnumerable<Intervencao> GetByProjeto(int idProjeto);


    }

    
}
