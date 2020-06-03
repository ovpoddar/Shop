using System.Collections.Generic;

namespace DataAccess.Entities
{
    public class Brand : BaseEntity
    {
        public string BrandName { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
