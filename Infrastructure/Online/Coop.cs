using FoodStore.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace FoodStore.Infrastructure.Online
{
    public class Coop : IProductImporter
    {

        private readonly List<Product> _outsideProducts = new List<Product>();
        private static string _cachedResult = "";

        public List<Product> GetProducts()
        {
            if (_cachedResult == "")
            {
                _cachedResult = WebWorker.DownloadPage("https://coopmad-website-prod-endpoint.azureedge.net/api/search/products?term=%2a&categories=5&lastFacet=sortby&sortby=Popularity&pageSize=21");
                //_cachedResult = WebWorker.DownloadPage("file:///C:/Users/catalin/Desktop/coop.json");
            }

            var o = JObject.Parse(_cachedResult);

            foreach(var p in o["products"])
            {
                _outsideProducts.Add(new Product
                {
                    Category = (string)p["category"],
                    Description = (string)p["spotText"],
                    Name = (string)p["displayName"],
                    Picture = (string)p["image"] + "width=130&height=130&mode=pad&format=jpg&bgcolor=ffffff",
                    Price = (decimal)p["salesPrice"]["amount"]
                });
            }

            return _outsideProducts;
        }
    }
}