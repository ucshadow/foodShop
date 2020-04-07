using FoodStore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodStore.Entities
{
    public class Cart
    {
        public readonly List<CartEntry> CartEntries = new List<CartEntry>();
        public void AddItem(Product product, int quantity)
        {
            CartEntry line = CartEntries
            .Where(p => p.Product.ProductID == product.ProductID)
            .FirstOrDefault();
            if (line == null)
            {
                CartEntries.Add(new CartEntry
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }
        public void RemoveLine(Product product)
        {
            CartEntries.RemoveAll(l => l.Product.ProductID == product.ProductID);
        }
        public decimal ComputeTotalValue()
        {
            return CartEntries.Sum(e => (e.Product.Discount > 0 ? 
            Helpers.CalculateDiscount(e.Product.Price, e.Product.Discount) :
            e.Product.Price) * e.Quantity);
        }
        public void Clear()
        {
            CartEntries.Clear();
        }
    }

    public class CartEntry
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
    
}
