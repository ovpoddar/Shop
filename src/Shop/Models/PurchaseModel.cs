using System.Collections.Generic;

namespace Shop.Models
{
    public class PurchaseModel
    {
        public List<ItemModel> Items { get; set; }
        public uint PaymentType { get; set; }
    }
}
