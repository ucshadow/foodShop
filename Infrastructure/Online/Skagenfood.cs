using FoodStore.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

namespace FoodStore.Infrastructure.Online
{
    public class Skagenfood : IProductImporter
    {

        private readonly List<Product> _outsideProducts = new List<Product>();
        private static string _cachedResult = "";


        public List<Product> GetProducts()
        {
            var baseUrl = "https://ny.skagenfood.dk/";
            if(_cachedResult == "")
            {
                _cachedResult = WebWorker.DownloadPage("https://ny.skagenfood.dk/api/headless/content/fetchcontentbykey?key=%2Fda-dk%2Ffiskekasser&includeGlobalData=true&format=json");
                //_cachedResult = WebWorker.DownloadPage("file:///C:/Users/catalin/Desktop/ass.json");
            }

            var o = JObject.Parse(_cachedResult);

            foreach(var entry in o["content"]["page"]["content"]["selectedSubscriptionPackages"])
            {
                var remoteProducts = entry["products"];

                var category = (string)entry["recipeType"]; // could be productCategory, the admin can edit it anyways.

                var pic = (string)entry["imageUrl"];

                // add the main product

                CreateProduct(remoteProducts[0], category, baseUrl, pic);

                foreach(var product in remoteProducts)
                {
                    foreach (var addon in product["addonProducts"])
                    {
                        if (addon != null)
                        {
                            CreateProduct(addon, "uncategorised", baseUrl, (string)addon["addonImageUrl"]);
                        }
                    }
                }               
                
            }

            return _outsideProducts;
        }

        private void CreateProduct(JToken jToken, string category = "unknown", string baseUrl = "https://ny.skagenfood.dk/", string pic = "")
        {
            if (ProductExistsByName((string)jToken["displayName"])) return;

            _outsideProducts.Add(new Product
            {
                Name = (string)jToken["displayName"] ?? "unknown",
                Description = (string)jToken["displayName"] ?? "unknown",
                Category = category ?? "unknown",
                Discount = 0,
                NumberOfVotes = 0,
                Picture = baseUrl + (string)pic,
                Price = jToken["salesPrice"]["value"] == null ? 0 : (decimal)jToken["salesPrice"]["value"],
                Quantity = 100,
                Rating = 0,
                Size = TryParseSize(jToken),
                Unit = TryParseUnit(jToken)
            });
        }

        private string TryParseUnit(JToken jToken)
        {
            if(jToken["additionalItems"] != null && jToken["additionalItems"].Count() > 0)
            {
                try
                {
                    return ((string)(jToken["additionalItems"][0])).Split(' ')[1];
                } 
                
                catch
                {
                    return "g";
                }
            }
            return "g";
        }

        private decimal TryParseSize(JToken jToken)
        {
            if (jToken["additionalItems"] != null && jToken["additionalItems"].Count() > 0)
            {
                try
                {
                    return Decimal.Parse(((string)(jToken["additionalItems"][0])).Split(' ')[0]);
                } 
                catch
                {
                    return 0;
                }
            }
            return 0;
        }

        private bool ProductExistsByName(string name)
        {
            if (name == null) return false;
            foreach(var p in _outsideProducts)
            {
                if(p.Name == name)
                {
                    return true;
                }
            }
            return false;
        }
    }
}