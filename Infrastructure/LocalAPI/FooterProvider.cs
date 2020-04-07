using FoodStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodStore.Infrastructure.LocalAPI
{
    public static class FooterProvider
    {
        private static Random _rnd = new Random();

        public static List<Product> GetFooterData()
        {
            var res = new List<Product>();

            if(GlobalProductCache.ProductCache.Count == 0)
            {
                return res;
            }

            for(var i = 0; i < 6; i++)
            {
                res.Add(GlobalProductCache.ProductCache[_rnd.Next(1, GlobalProductCache.ProductCache.Count - 1)]);
            }
            return res;
        }

        public static string FormatHref(string name)
        {
            return "/Product/Details?productName=" + name;
        }
    }
}