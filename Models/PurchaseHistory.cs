using FoodStore.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

namespace FoodStore.Models
{
    public class PurchaseHistory
    {
        public IEnumerable<Purchase> History { get; }

        public PurchaseHistory(IEnumerable<Purchase> history)
        {
            History = history;
        }

        public decimal CalculateTotalPrice()
        {
            return History.Sum(e => e.Price * e.ProductCount);
        }

        public decimal CalculateTotalPrice(List<Purchase> p)
        {
            return p.Sum(e => e.Price * e.ProductCount);
        }

        public Dictionary<string, List<Purchase>> SortedByDate()
        {
            var res = new Dictionary<string, List<Purchase>>();
            foreach(var h in History)
            {
                if(res.ContainsKey(h.PurchaseDate))
                {
                    res[h.PurchaseDate].Add(h);
                } 
                else
                {
                    res.Add(h.PurchaseDate, new List<Purchase>() { h });
                }
            }
            return res;
        }
    }
}