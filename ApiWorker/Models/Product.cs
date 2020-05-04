using System.Collections.Generic;

namespace CheckoutSimulator.Models
{
    public class Product 
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string BarCode { get; set; }
        public decimal Price { get; set; }
        public double WholesalePrice { get; set; }
        public int StockLevel { get; set; }
        public double OrderLevel { get; set; }
        public double MinimumWholesaleOrder { get; set; }
        public int CategoriesId { get; set; }
       
    }
}
