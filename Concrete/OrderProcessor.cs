using FoodStore.Entities;
using FoodStore.Abstract;
using System;
using FoodStore.Infrastructure;
using FoodStore.Infrastructure.LocalAPI;
using FoodStore.Infrastructure.Cache;
using FoodStore.Models;

namespace FoodStore.Concrete
{
    public class OrderProcessor : IOrderProcessor
    {
        private readonly IPurchaseHistoryRepository _prRepository;
        private readonly IProductRepository _pRepository;

        public OrderProcessor(IPurchaseHistoryRepository repository, IProductRepository productRepository)
        {
            _prRepository = repository;
            _pRepository = productRepository;
        }

        public void ProcessOrder(Cart cart, ShippingDetails shippingDetails, string userId)
        {
            cart.CartEntries.ForEach(e => {
                var ph = new Purchase
                {
                    PurchaseDate = DateTime.Now.ToString(),
                    ShippingDetails = shippingDetails,
                    ProductName = e.Product.Name,
                    ProductCount = e.Quantity,
                    UserId = userId,
                    Price = e.Product.Discount > 0 ? Helpers.CalculateDiscount(e.Product.Price, e.Product.Discount) : e.Product.Price,
                    ProductId = e.Product.ProductID,
                    AffiliateId = e.Product.AffiliateId
                };
                e.Product.Quantity -= e.Quantity;
                _pRepository.SaveProduct(e.Product);
                GlobalProductCache.UpdateProduct(e.Product);
                RealTimeSellData.ReplaceOrUpdateTrackedProduct(e.Product, e.Quantity);
                GlobalCache.GetCache().ClearCachedItem<ProductModel>(e.Product.ProductID);
                _prRepository.SavePurchase(ph);
            });
        }

        // affiliate orders only come in one at a time
        public void ProcessAffiliateOrder(Product e, ShippingDetails shippingDetails, string affiliateId)
        {
            var ph = new Purchase
            {
                PurchaseDate = DateTime.Now.ToString(),
                ShippingDetails = shippingDetails,
                ProductName = e.Name,
                ProductCount = 1,
                UserId = affiliateId,
                Price = e.Discount > 0 ? Helpers.CalculateDiscount(e.Price, e.Discount) : e.Price,
                ProductId = e.ProductID,
                AffiliateId = e.AffiliateId,
            };
            e.Quantity -= 1;
            _pRepository.SaveProduct(e);
            GlobalProductCache.UpdateProduct(e);
            RealTimeSellData.ReplaceOrUpdateTrackedProduct(e, 1);
            GlobalCache.GetCache().ClearCachedItem<ProductModel>(e.ProductID);
            _prRepository.SavePurchase(ph);
        }
    }
}
