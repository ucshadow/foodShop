using FoodStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodStore.Infrastructure.Online
{
    public interface IProductImporter
    {
        List<Product> GetProducts();
    }
}
