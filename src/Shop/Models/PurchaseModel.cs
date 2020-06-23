using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Models
{
    public class PurchaseModel
    {
        public List<ItemModel> Items { get; set; }
        public uint PaymentType { get; set; }
    }
}
