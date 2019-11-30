using Shop.Entities;
using Shop.Repositories;
using Shop.ViewModels;
using System;
using System.Linq;

namespace Shop.Handlers
{
    public class WholesaleHandler : IWholesaleHandler
    {

        private readonly IProductHandler ProductHandler;
        private readonly IGenericRepository<ProductWholeSale> GenericRepository;
        private readonly IGenericRepository<WholesaleSize> Repository;

        public WholesaleHandler(IGenericRepository<WholesaleSize> repository, IProductHandler productHandler , IGenericRepository<ProductWholeSale> genericRepository)
        {
            Repository = repository ?? throw new ArgumentNullException(nameof(Repository));
            ProductHandler = productHandler ?? throw new ArgumentNullException(nameof(ProductHandler));
            GenericRepository = genericRepository ?? throw new ArgumentNullException(nameof(GenericRepository));
        }

        public WholeSaleViewModel GetModel() => new WholeSaleViewModel
        {
            Productnames = ProductHandler.Products()
        };

        public bool Add(WholeSaleViewModel Details, int Products)
        {
            int no = Repository.GetAll().Count() + 1;
            if (Details.Package == 0 || Details.Size == 0)
            {
                return false;
            }
            else
            {
                WholesaleSize wholesale = new WholesaleSize
                {
                    Package = Details.Package,
                    Size = Details.Size,
                };
                Repository.Add(wholesale);
                ProductWholeSale product = new ProductWholeSale
                {
                    ProductId = Products,
                    WholesaleSizeId = no
                };
                GenericRepository.Add(product);
                return true;
            }
        }
    }
}
