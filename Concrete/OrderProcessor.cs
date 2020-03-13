using FoodStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodStore.Domain.Concrete
{
    public class OrderProcessor : IOrderProcessor
    {
        public void ProcessOrder(Cart cart, ShippingDetails shippingDetails)
        {
            Console.WriteLine(cart);
            Console.WriteLine(shippingDetails);
        }
    }
}
