using Shop.Entities;
using Shop.Handlers.Interfaces;
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

        public void AddBalance(Balance Balance)
        {
            _repository.Add(Balance);
            _repository.Save();
        }

        public Balance GetLastBalance() =>
            _repository.GetAll()
            .Where(o => o.Id == _repository.GetAll().Count())
            .FirstOrDefault();
    }
}
