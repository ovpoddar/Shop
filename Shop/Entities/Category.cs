using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shop.Entities
{
    public class Category : BaseEntity
    {
        public int? ParentId { get; set; }
        public Category Parent { get; set; }
        public string Name { get; set; }
        public ICollection<Category> SubCategories { get; set; }
    }
}
