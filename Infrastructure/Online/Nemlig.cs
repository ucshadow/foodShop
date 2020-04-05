using FoodStore.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodStore.Infrastructure.Online
{
    public class Nemlig : IProductImporter
    {
        private readonly List<Product> _outsideProducts = new List<Product>();
        private static string _cachedResult = "";

        public List<Product> GetProducts()
        {
            if (_cachedResult == "")
            {
                // this is more like a snapshot since it uses timestamps, but for demo purpose only, I will leave it like it is
                _cachedResult = WebWorker.DownloadPage("https://www.nemlig.com/webapi/4CbKt3-4-ILu11lvZ/2020040608-180-600/1/0/Products/GetByProductGroupId?pageindex=-1&pagesize=-1&productGroupId=f4824940-3647-4ab5-9af0-6e48635c11bb");
            }

            var o = JObject.Parse(_cachedResult);

            foreach (var p in o["Products"])
            {
                _outsideProducts.Add(new Product
                {
                    Category = (string)p["ProductCategoryGroupName"],
                    Description = (string)p["Description"],
                    Name = (string)p["Name"],
                    Picture = (string)p["PrimaryImage"],
                    Price = (decimal)p["UnitPriceCalc"],
                    Unit = (string)p["UnitPriceLabel"],
                });
            }

            return _outsideProducts;
        }
    }
}