using Shop.Entities;
using Shop.Repositories;
using System;
using System.Linq;

namespace Shop.Handlers
{
    public class BalanceHandler : IBalanceHandler
    {
        private readonly IGenericRepository<Balance> _repository;
        public BalanceHandler(IGenericRepository<Balance> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(_repository));
        }
        
        public bool AddBalance(Balance Balance)
        {
            _repository.Add(Balance);
            _repository.Save();
            return true;
        }

        public Balance GetLastBalance() => _repository.GetAll().Where(o => o.Id == _repository.GetAll().Count()).FirstOrDefault();
    }
}
