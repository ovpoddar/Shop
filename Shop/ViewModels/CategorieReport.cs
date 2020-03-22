using Shop.Entities;
using System.Collections.Generic;

namespace Shop.ViewModels
{
    public class CategorieReport
    {
        public bool Success { get; set; }
        public List<Category> All { get; set; }
    }
}
