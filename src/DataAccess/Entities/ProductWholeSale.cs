namespace DataAccess.Entities
{
    public class ProductWholeSale : BaseEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int WholesaleSizeId { get; set; }
        public WholesaleSize WholesaleSize { get; set; }
    }
}
