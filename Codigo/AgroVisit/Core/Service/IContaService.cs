using Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service
{
    public interface IContaService
    {
        public int Create(Conta conta);
        public void Edit(Conta conta);
        public void Delete(int id);
        public Conta Get(int id);
        public IEnumerable<Conta> GetAll();
        public IEnumerable<ContaDTO> GetByCliente(string nomeCliente);
        public IEnumerable<ContaDTO> GetByDataPagamentoPrevista(DateTime data);
    }
}
