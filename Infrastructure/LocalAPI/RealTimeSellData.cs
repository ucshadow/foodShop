﻿using FoodStore.Domain.Concrete;
using FoodStore.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web;

namespace FoodStore.Infrastructure.LocalAPI
{
    public class RealTimeSellData
    {
        private readonly Random _rnd = new Random();
        public static List<TrackedProduct> TrackedProducts = new List<TrackedProduct>();        
        public static List<Purchase> LastSold { get; set; }
        private readonly EFDbContext _context;

        public RealTimeSellData()
        {
            _context = new EFDbContext();

            // loading all the history is not good..but works for now
            LastSold = _context.PurchaseHistory
              .GroupBy(e => e.ProductId) // remove duplicates so we wont end up with 4 asparagus pictures :)
              .Select(e => e.FirstOrDefault())
              .OrderByDescending(e => e.PurchaseID)
              .Take(4).ToList();
           
            Init();
        }

        private void Init()
        {
            foreach(var history in LastSold)
            {
                var product = _context.Products.FirstOrDefaultAsync(e => e.ProductID == history.ProductId);

                if(product.Result == null)
                {
                    AddTrackedProduct(_context.Products.ElementAt(_rnd.Next(0, 700)));
                    return;
                }
                AddTrackedProduct(product.Result);
                
            }
        }

        public void AddTrackedProduct(Product product)
        {
            TrackedProducts.Add(new TrackedProduct
            {
                Remaining = product.Quantity,
                Product = product,
                ID = _rnd.Next()
            });
        }

        public static void ReplaceOrUpdateTrackedProduct(Product product, int quantity)
        {

            // if product exists, just update the counter

            for(var i = 0; i < TrackedProducts.Count(); i++)
            {
                if(TrackedProducts[i].Product.ProductID == product.ProductID)
                {
                    TrackedProducts[i].Remaining -= quantity;
                    return;
                }
            }

            // remove oldest 
            // could also do it by PurchaseDate, but this should work just fine
            var removedIndex = FindAndRemoveMin();
            TrackedProducts.Insert(removedIndex, new TrackedProduct
            {
                Remaining = product.Quantity,
                Product = product,
                ID = product.ProductID + 100000, // :D
                AddedAt = DateTime.Now
            });
        }

        private static int FindAndRemoveMin()
        {
            var curMin = DateTime.Now;
            foreach(var t in TrackedProducts)
            {
                if(t.AddedAt <= curMin)
                {
                    curMin = t.AddedAt;
                }
            }

            for(var i = 0; i < TrackedProducts.Count; i++)
            {
                if(TrackedProducts.ElementAt(i).AddedAt == curMin)
                {
                    TrackedProducts.RemoveAt(i);
                    return i;
                }
            }
            return 0;
        }
    }    

    public class TrackedProduct
    {
        public Product Product { get; set; }
        public int Remaining { get; set; }
        public int ID { get; set; }
        public DateTime AddedAt { get; set; }
    }
}