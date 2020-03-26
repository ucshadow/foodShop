using FoodStore.Concrete;
using FoodStore.Domain.Concrete;
using FoodStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodStore.Infrastructure.LocalAPI
{
    public static class GlobalCache
    {
        public static List<Product> ProductCache;

        public static void SetUp()
        {
            var products = new EFProductRepository();
            ProductCache = products.GetClone();
        }
    }
}