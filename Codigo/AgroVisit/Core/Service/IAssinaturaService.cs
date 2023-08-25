using Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service
{
    public interface IAssinaturaService
    {
        public int Create(Assinatura assinatura);
        public void Edit(Assinatura assinatura);
        public void Delete(int id);
        public Assinatura Get(int id);
        public IEnumerable<Assinatura> GetAll();
        public IEnumerable<AssinaturaDTO> GetByNome(string nome);
    }
}
