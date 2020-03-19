using System;
using System.Collections.Generic;
using System.Data.SqlTypes;

namespace FoodStore.Entities
{
    public class Purchase
    {
        public int PurchaseID { get; set; }
        public string UserId { get; set; }
        public string ProductName { get; set; }
        public int ProductCount { get; set; }
        public String PurchaseDate { get; set; }
        public ShippingDetails ShippingDetails { get; set; }
        public decimal Price { get; set; }

    }
}