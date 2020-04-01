using Shop.Entities;
using System.Collections.Generic;

namespace Shop.Models
{
    public class CategorieModel
    {
        public bool Success { get; set; }
        public List<Category> All { get; set; }
    }
}
