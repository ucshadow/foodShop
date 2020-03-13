using FoodStore.Entities;
using System.Collections.Generic;

namespace FoodStore
{
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }

        void SaveProduct(Product product);

        Product DeleteProduct(int productID);
    }
}
