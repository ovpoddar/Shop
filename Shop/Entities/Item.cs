namespace Shop.Entities
{
    public class Item : BaseEntity
    {
        public int No { get; set; }
        public int ProductId { get; set; }
        public uint Quantity { get; set; }
        public Product Product { get; set; }
        public Balance Balance { get; set; }
    }
}
