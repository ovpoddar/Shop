using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shop.Entities
{
    public class WholesaleSize : BaseEntity
    {
        public int Size { get; set; }
        public int Package { get; set; }
        public List<ProductWholeSale> ProductWholeSales { get; set; }
    }
}
