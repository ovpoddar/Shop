namespace Shop.Models
{
    public class SaleProduct
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string BarCode { get; set; }
        public decimal Price { get; set; }
        public int SaleQuantity { get; set; }
        public bool CancelItem { get; set; }
        public int StockLevel { get; set; }
        public double OrderLevel { get; set; }
        public string Message { get; set; }
        public int CategoriesId { get; set; }
        public string Category { get; set; }
        public int BrandId { get; set; }
        public string Brand { get; set; }
    }
}
