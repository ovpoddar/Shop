using System.Collections.Generic;

namespace DataAccess.Entities
{
    public class Category : BaseEntity
    {
        public int? ParentId { get; set; }
        public Category Parent { get; set; }
        public string Name { get; set; }
        public ICollection<Category> SubCategories { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
