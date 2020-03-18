using FoodStore.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web;

namespace FoodStore.Infrastructure.LocalAPI
{
    public class RealTimeSellData
    {
        private readonly Random _rnd = new Random();
        public static List<TrackedProduct> P = new List<TrackedProduct>();
        private int _updateIntervalInSeconds = 3;
        public static List<Product> Products;
        private Thread _t;

        // cast a chance that a tracked product is sold or a new one is added
        // either update tracked counter or replace the lowest

        public void Loop()
        {
            if (_t != null) return;
            _t = new Thread(() =>
            {
                while (true)

                {
                    while (P.Count() < 4)
                    {
                        AddTrackedProduct();
                    }
                    DecayProductLife();
                    Sell();
                    Thread.Sleep(_updateIntervalInSeconds * 1000);
                }

            });
            _t.Start();
        }

        private void DecayProductLife()
        {
            foreach (var p in P)
            {
                p.LifeTime -= _rnd.Next(1, 20);
                if(p.LifeTime <= 0)
                {
                    P.Remove(p); // should I do a custom remove or can C# handle it?
                }
            }
        }

        private void Sell()
        {
            var r = _rnd.Next(1, 100);
            if(r < 20)
            {
                // replace oldest product
                ReplaceOldest();
            }
            else
            {
                // update a product already tracked
                for(var i = 0; i < _rnd.Next(0, P.Count()); i++)
                {
                    P.ElementAt(_rnd.Next(0, 4)).SellCount += _rnd.Next(1, 4);
                }
            }
        }

        private void ReplaceOldest()
        {
            P.RemoveAt(P.IndexOfMin());
            AddTrackedProduct();
        }

        private void AddTrackedProduct()
        {
            var p = new TrackedProduct
            {
                SellCount = _rnd.Next(10, 200),
                LifeTime = 100000,
                Product = Products.ElementAt(_rnd.Next(0, Products.Count())),
                ID = _rnd.Next()
            };

            if (P.Contains(p)) AddTrackedProduct();

            P.Add(p);
        }
    }    

    public class TrackedProduct
    {
        public Product Product { get; set; }
        public int SellCount { get; set; }
        public int LifeTime { get; set; }
        public int ID { get; set; }

        public override bool Equals(object obj)
        {
            var item = obj as TrackedProduct;

            if (item == null)
            {
                return false;
            }

            return ID == item.ID;
        }

        public override int GetHashCode()
        {
            return this.GetHashCode();
        }
    }

    public static class Extension_
    {
        public static int IndexOfMin(this IList<TrackedProduct> self)
        {
            if (self == null)
            {
                throw new ArgumentNullException("self");
            }

            if (self.Count == 0)
            {
                throw new ArgumentException("List is empty.", "self");
            }

            var min = self[0];
            int minIndex = 0;

            for (int i = 1; i < self.Count; ++i)
            {
                if (self[i].LifeTime < min.LifeTime)
                {
                    min = self[i];
                    minIndex = i;
                }
            }

            return minIndex;
        }
    }
}