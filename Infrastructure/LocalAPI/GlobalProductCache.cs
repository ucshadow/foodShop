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
    }
}