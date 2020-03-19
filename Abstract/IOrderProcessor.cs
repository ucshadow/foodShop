using FoodStore.Entities;

namespace FoodStore.Abstract
{
    public interface IOrderProcessor
    {
        void ProcessOrder(Cart cart, ShippingDetails shippingDetails, string userId);
    }
}
