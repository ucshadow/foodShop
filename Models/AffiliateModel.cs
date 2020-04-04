using FoodStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodStore.Models
{
    public class AffiliateModel
    {
        public Affiliate Affiliate { get; set; }
        public List<AffiliateProduct> AffiliateProducts { get; set; } = new List<AffiliateProduct>();
        public Dictionary<string, List<SellData>> AffiliateProductSales { get; set; } = new Dictionary<string, List<SellData>>();
    }

    public class SellData
    {
        public int Count { get; set; }
        public decimal Price { get; set; }
        public string Date { get; set; }
    }
}

