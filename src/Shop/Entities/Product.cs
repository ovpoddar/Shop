using System.Collections.Generic;

namespace Shop.Entities
{
    public class Product : BaseEntity
    {
        public string ProductName { get; set; }
        public string BarCode { get; set; }
        public decimal Price { get; set; }
        public double WholesalePrice { get; set; }
        public uint StockLevel { get; set; }
        public double OrderLevel { get; set; }
        public double MinimumWholesaleOrder { get; set; }
        public int CategoriesId { get; set; }
        public Category Categories { get; set; }
        public int BrandId { get; set; }
        public Brand Brands { get; set; }
        public List<ProductWholeSale> ProductWholeSales { get; set; }
        public virtual ICollection<Balance> Balances { get; set; }
    }
}
