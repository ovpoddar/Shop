using System.Collections.Generic;

namespace DataAccess.Entities
{
    public class WholesaleSize : BaseEntity
    {
        public int Size { get; set; }
        public int Package { get; set; }
        public List<ProductWholeSale> ProductWholeSales { get; set; }
    }
}
