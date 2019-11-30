using Shop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.ViewModels
{
    public class CategoryViewModel
    {
        public string Name { get; set; }
        public int? Id { get; set; }
        public List<Category> Categories { get; set; }
    }
}
