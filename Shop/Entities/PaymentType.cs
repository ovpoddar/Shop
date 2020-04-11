namespace Shop.Entities
{
    public class PaymentType: BaseEntity
    {
        public string Name { get; set; }
        public Balance Balance { get; set; }
    }
}
