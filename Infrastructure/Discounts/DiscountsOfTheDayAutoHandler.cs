using FoodStore.Abstract;
using FoodStore.Concrete;
using FoodStore.Domain.Concrete;
using FoodStore.Entities;
using FoodStore.Infrastructure.LocalAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace FoodStore.Infrastructure.Discounts
{
    // the last updated time should be stored in the database in case of crashes and all that
    public class DiscountsOfTheDayAutoHandler : IDiscountsManager
    {
        private readonly List<Product> _discountedProducts;
        public int MaxProductsDiscounted = 5;
        public int MaxDiscountPercentage = 30;
        public int CheapBias = 50; // 0 == No bias; 100 = most of the discounts will be on the low discount side 
        public int UpdateTimeInSeconds = 60 * 5; // updates every n seconds
        private readonly IProductRepository _pRepository;
        private readonly List<Product> _cachedProducts;
        private readonly Random _rnd;
        public bool Looping = true;

        public DiscountsOfTheDayAutoHandler()
        {
            _pRepository = DependencyResolver.Current.GetService<IProductRepository>();
            _discountedProducts = new List<Product>();
            _cachedProducts = GlobalProductCache.ProductCache;
            _rnd = new Random();

            new Thread(() => {
                while(Looping)
                {
                    Loop();
                    Thread.Sleep(UpdateTimeInSeconds * 1000);
                }
            }).Start();
        }

        public List<Product> GetDiscounts()
        {
            return _discountedProducts;
        }

        private void Loop()
        {
            ClearPreviousDiscounts();
            AddDiscounts();

        }

        private void AddDiscounts()
        {
            for (var i = 0; i < MaxProductsDiscounted; i++)
            {
                // in it's current form, duplicates cann appear, but I am not going to modify it,
                // at least not now
                var index = _rnd.Next(0, _cachedProducts.Count());
                var product = _cachedProducts.ElementAt(index);
                AddItem(product);
            }
        }

        public void AddItem(Product product, bool saveItem = true)
        {
            product.Discount = CalculateDiscountRate();
            _discountedProducts.Add(product);
            if(saveItem)
            {
                _pRepository.SaveProduct(product);
            }
        }

        private decimal CalculateDiscountRate()
        {
            var baseDiscountPercentage = _rnd.Next(1, MaxDiscountPercentage);
            int cheapDiscount;
            if (CheapBias == 1 || CheapBias == 0)
            {
                cheapDiscount = 0;
            } 
            else
            {
                cheapDiscount = _rnd.Next((CheapBias / 2), CheapBias);
            }
            return baseDiscountPercentage - (baseDiscountPercentage * cheapDiscount / 100);
        }

        private void ClearPreviousDiscounts()
        {
            foreach(var p in _discountedProducts)
            {
                p.Discount = 0;
                _pRepository.SaveProduct(p);
            }
            _discountedProducts.Clear();
        }

        public void RemoveProductIfExists(int productID)
        {
            for(int i = 0; i < _discountedProducts.Count(); i++)
            {
                if(_discountedProducts[i].ProductID == productID)
                {
                    _discountedProducts.RemoveAt(i);
                    return;
                }
            }
        }

        public bool ProductExists(int productID)
        {
            return _discountedProducts.FirstOrDefault(e => e.ProductID == productID) != null;
        }

        public void SetMaxDiscountPercentage(int? percentage)
        {
            if (percentage > 100) percentage = 100;
            if (percentage < 1) percentage = 1; 
            MaxDiscountPercentage = percentage.Value;
        }

        public void SetMaxProductsDiscounted(int? maxProducts)
        {
            if (maxProducts >= _cachedProducts.Count) maxProducts = _cachedProducts.Count();
            if (maxProducts < 0) maxProducts = 0;
            MaxProductsDiscounted = maxProducts.Value;
        }

        public void SetCheapBias(int? cheapBias)
        {
            if (cheapBias < 0) cheapBias = 0;
            if (cheapBias > 100) cheapBias = 100;
            CheapBias = cheapBias.Value;
        }

        public void SetUpdateTimeInSeconds(int? seconds)
        {
            if (seconds < 5) seconds = 5;
            UpdateTimeInSeconds = seconds.Value;
        }

        public void TriggerManualUpdate()
        {
            Loop();
        }

        public int GetMaxDiscountPercentage()
        {
            return MaxDiscountPercentage;
        }

        public int GetMaxProductsDiscounted()
        {
            return MaxProductsDiscounted;
        }

        public int GetCheapBias()
        {
            return CheapBias;
        }

        public int GetUpdateTimeInSeconds()
        {
            return UpdateTimeInSeconds;
        }
    }
}