﻿using FoodStore.Entities;
using System.Collections.Generic;

namespace FoodStore.Abstract
{
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }

        void SaveProduct(Product product);

        Product DeleteProduct(int productID);

        List<Product> GetClone();
    }
}
