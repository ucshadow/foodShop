using FoodStore.Entities;
using FoodStore.Abstract;
using System;
using FoodStore.Infrastructure;

namespace FoodStore.Concrete
{
    public class OrderProcessor : IOrderProcessor
    {
        private readonly IPurchaseHistoryRepository _pRepository;

        public OrderProcessor(IPurchaseHistoryRepository repository)
        {
            _pRepository = repository;
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
                    ProductId = e.Product.ProductID
                };
                _pRepository.SavePurchase(ph);
            });
        }
    }
}
