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
        public uint Create(Projeto projeto);
        public void Edit(Projeto projeto);
        public void Delete(uint id);
        public Projeto Get(uint id);
        public IEnumerable<Projeto> GetAll();
        public IEnumerable<ProjetoDTO> GetByNome(string nome);
        public IEnumerable<ProjetoDTO> GetByData(DateTime data);
        public IEnumerable<ProjetoDTO> GetByStatus(string status);
        public IEnumerable<Projeto> GetByPropriedade(uint idPropriedade);
        public IEnumerable<IntervencaoDTO> GetAllIntervencoes(uint idPropriedade);
        public IEnumerable<ContaDTO> GetAllConta(uint idPropriedade);
    }
}