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

        public bool Add(WholeSaleViewModel details)
        {
            if (details.Package == 0 || details.Size == 0 ||
                _repository.GetAll().Any(o => o.Package == details.Package && o.Size == details.Size))
                return false;

            _repository.Add(new WholesaleSize
            {
                Package = details.Package,
                Size = details.Size,
            });

            _genericRepository.Save();

            return true;
        }

        public int GetId(int size, int package) =>
            _repository.GetAll()
            .Where(o => o.Size == size && o.Package == package)
            .Select(o => o.Id)
            .FirstOrDefault();
    }
}
