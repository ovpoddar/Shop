using Shop.Entities;
using System.Collections.Generic;

namespace Shop.ViewModels
{
    public class CategoryViewModel
    {
        public string Name { get; set; }
        public int? Id { get; set; }
        public List<Category> Categories { get; set; }
    }
}
