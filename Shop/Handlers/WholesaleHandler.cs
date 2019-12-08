using Shop.Entities;
using Shop.Repositories;
using Shop.ViewModels;
using System;
using System.Linq;

namespace Shop.Handlers
{
    public class WholesaleHandler : IWholesaleHandler
    {
        private readonly IGenericRepository<ProductWholeSale> _genericRepository;
        private readonly IGenericRepository<WholesaleSize> _repository;

        public WholesaleHandler(IGenericRepository<WholesaleSize> repository,
            IGenericRepository<ProductWholeSale> genericRepository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(_repository));
            _genericRepository = genericRepository ?? throw new ArgumentNullException(nameof(_genericRepository));
        }

        public bool Add(WholeSaleViewModel Details)
        {
            if (Details.Package == 0 || Details.Size == 0)
                return false;

            var wholesale = new WholesaleSize
            {
                Package = Details.Package,
                Size = Details.Size,
            };

            _repository.Add(wholesale);
            _genericRepository.save();
            return true;
        }
    }
}
