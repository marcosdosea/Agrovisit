﻿namespace Core.Service
{
    public interface IContaService
    {
        public int Create(Conta conta);
        public void Edit(Conta conta);
        public void Delete(int id);
        public Conta Get(int id);
        public IEnumerable<Conta> GetAll();
        public IEnumerable<Conta> GetByCliente(string nomeCliente);
        public IEnumerable<Conta> GetByDataPagamentoPrevista(DateTime data);
    }
}
