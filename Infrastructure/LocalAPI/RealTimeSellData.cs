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
        public static List<TrackedProduct> _p = new List<TrackedProduct>();
        private int _updateIntervalInSeconds = 5;
        private int _replaceChance = 30; // percentage chance of a new item to be added
        private readonly List<Product> _products;
        private Thread _t;

        public RealTimeSellData(IProductRepository repository)
        {
            _products = repository.GetClone();
            Loop();
        }

        public void Loop()
        {
            if (_t != null) return;
            _t = new Thread(() =>
            {
                while (true)

                {
                    while (_p.Count() < 4)
                    {
                        _p.Add(new TrackedProduct
                        {
                            SellCount = _rnd.Next(10, 200),
                            LifeTime = 10000,
                            Product = _products.ElementAt(_rnd.Next(0, _products.Count()))
                        });
                    }
                    DecayProductLife();
                    HandleReplaceChance();
                    Thread.Sleep(_updateIntervalInSeconds * 1000);
                }

            });
            _t.Start();
        }

        private void DecayProductLife()
        {
            foreach (var p in _p)
            {
                p.LifeTime -= 1;
                if(p.LifeTime <= 0)
                {
                    _p.Remove(p); // should I do a custom remove or can C# handle it?
                }
            }
        }

        private void HandleReplaceChance()
        {
            var r = _rnd.Next(1, 100);
            if(r < _replaceChance)
            {
                _p.Sort((a, b) => a.LifeTime - b.LifeTime);
                _p.RemoveAt(0); // todo is it the other end? :D
            }
        }
    }    

    public class TrackedProduct
    {
        public Product Product { get; set; }
        public int SellCount { get; set; }
        public int LifeTime { get; set; }
    }
}