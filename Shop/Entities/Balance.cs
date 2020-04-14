﻿using System;

namespace Shop.Entities
{
    public class Balance : BaseEntity
    {
        public DateTime Date { get; set; }
        public decimal Incoming { get; set; }
        public decimal Outgoing { get; set; }
        public uint Quantity { get; set; }
        public decimal Ammount { get; set; }
        public int? ProductId { get; set; }
        public Product Product { get; set; }
        public int PaymentTypeId { get; set; }
        public PaymentType PaymentType { get; set; }
    }
}
