using FoodStore.Abstract;
using FoodStore.Concrete;
using FoodStore.Domain.Concrete;
using FoodStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodStore.Infrastructure.LocalAPI
{
    public static class GlobalProductCache
    {
        public static List<Product> ProductCache;

        public static void SetUp()
        {
            var products = DependencyResolver.Current.GetService<IProductRepository>();
            ProductCache = products.GetClone();
        }

        public static void UpdateProduct(Product p)
        {
            for(var i = 0; i < ProductCache.Count(); i++)
            {
                if(ProductCache[i].ProductID == p.ProductID)
                {
                    ProductCache[i] = p;
                    return;
                }
            }
        }
    }
}