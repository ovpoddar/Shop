using Checkout.Models;
using Shop.Models;
using System.Collections.Generic;

namespace Shop.ViewModels
{
    public class PaymentViewModel
    {
        public List<ItemModel> Items { get; set; }
        public decimal Total { get; set; }
    }
}
