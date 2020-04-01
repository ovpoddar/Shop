using Shop.Handlers;
using Shop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Managers
{
    public class PaymentManager : IPaymentManager
    {
        private readonly IProductHandler _product;

        public PaymentManager(IProductHandler product)
        {
            _product = product ?? throw new ArgumentNullException(nameof(_product));
        }
        public ItemViewModel GetViewModels(string qunatity, string productId)
        {
            var amnt = Convert.ToInt32(qunatity);
            var product = _product.GetProduct(Convert.ToInt32(productId));
            return new ItemViewModel
            {
                Id = product.Id,
                Brand = product.Brand.BrandName,
                Name = product.ProductName,
                Price = product.Price,
                Quantity = amnt,
                TotalPrice = Convert.ToDouble(product.Price * amnt)
            };
        }
    }
}
