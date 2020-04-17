namespace Shop.Models
{
    public class SaleProduct
    {
        public int ProductId { get; set; }
        public int SaleQuantity { get; set; }
        public bool CancelItem { get; set; }
        public int StockLevel { get; set; }
        public double OrderLevel { get; set; }
        public string Message { get; set; }
       
    }
}
