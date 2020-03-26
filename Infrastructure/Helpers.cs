using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodStore.Infrastructure
{
    public static class Helpers
    {
        public static decimal CalculateDiscount(decimal price, decimal discountPercentage)
        {
            return decimal.Round(price - price * discountPercentage / 100, 2, MidpointRounding.AwayFromZero);
        }
    }
}