using DataAccess.Entities;
using System.Collections.Generic;

namespace Shop.ViewModels
{
    public class ProductListViewModel
    {
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
        public int TotalNo { get; set; }
    }
}
