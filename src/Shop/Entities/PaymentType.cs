using System.Collections.Generic;

namespace Shop.Entities
{
    public class PaymentType : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<Balance> Balance { get; set; }
    }
}
