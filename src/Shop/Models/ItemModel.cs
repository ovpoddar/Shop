﻿namespace Shop.Models
{
    public class ItemModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Brand { get; set; }
        public double TotalPrice { get; set; }
        public int Quantity { get; set; }
    }
}
