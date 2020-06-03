using System.Collections.Generic;

namespace DataAccess.Entities
{
    public class PaymentType : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<Balance> Balance { get; set; }
    }
}
