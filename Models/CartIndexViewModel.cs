﻿using FoodStore.Entities;
using FoodStore.Infrastructure;
using System.Diagnostics;

namespace FoodStore.Models
{
    public class CartIndexViewModel
    {
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }

        public decimal CalculateProductPriceWithDiscount(Product p)
        {
            if(p.Discount > 0)
            {
                return Helpers.CalculateDiscount(p.Price, p.Discount);
            }
            return p.Price;
        }
    }
}