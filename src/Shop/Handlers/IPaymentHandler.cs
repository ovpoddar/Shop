using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Handlers
{
    public interface IPaymentHandler
    {
        Task<bool> PurchaseCall(List<SaleProduct> products);
    }
}
