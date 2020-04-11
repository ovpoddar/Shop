using System;
using System.Collections.Generic;

namespace Shop.Entities
{
    public class Balance : BaseEntity
    {
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public decimal Incoming { get; set; }
        public decimal Outgoing { get; set; }
        public decimal Ammount { get; set; }
        public int PaymentTypeId { get; set; }
        public PaymentType PaymentType { get; set; }
        public ICollection<Item> Items { get; set; }
    }
}
