using Shop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.ViewModels
{
    public class WholeSaleViewModel
    {
        public List<Product> Productnames { get; set; }
        public int Size { get; set; }
        public int Package { get; set; }
    }
}
