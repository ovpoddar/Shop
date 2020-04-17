//using Shop.Handlers;
//using Shop.ViewModels;
//using System;
//using System.Collections.Generic;
//using System.Net.Http;
//using System.Threading.Tasks;

//namespace Shop.Managers
//{
//    public class PaymentManager : IPaymentManager
//    {
//        private readonly IProductHandler _product;

//        public PaymentManager(IProductHandler product)
//        {
//            _product = product ?? throw new ArgumentNullException(nameof(_product));
//        }
//        public ItemViewModel GetViewModels(string qunatity, string productId)
//        {
//            var product = _product.GetProduct(Convert.ToInt32(productId));
//            return new ItemViewModel
//            {
//                Id = product.Id,
//                Brand = product.Brands.BrandName,
//                Name = product.ProductName,
//                Price = product.Price,
//                Quantity = Convert.ToInt32(qunatity),
//                TotalPrice = Convert.ToDouble(product.Price * Convert.ToInt32(qunatity))
//            };
//        }

//        public async void PurchaseCall(List<ItemViewModel> models)
//        {
//            foreach(var item in models)
//            {
//                await new HttpClient().PostAsync("http://localhost:59616/api/Buy?id=" + item.Id + "&Qunatity=" + item.Quantity, null);
//            }
//        }
//    }
//}
