using Shop.Entities;
using Shop.Repositories;
using Shop.ViewModels;
using System.Collections.Generic;

namespace Shop.Handlers
{
    public class BalanceHandler : IBalanceHandler
    {
        private readonly IGenericRepository<Balance> _repository;

        public BalanceHandler(IGenericRepository<Balance> repository)
        {
            _repository = repository ?? throw new System.ArgumentNullException(nameof(_repository));
        }

        public void Purchase()
        {
            throw new System.NotImplementedException();
            //all done
        }

        public void Sales(List<ItemViewModel> products, string purchaseType)
        {
            throw new System.NotImplementedException();
        }
    }
}
