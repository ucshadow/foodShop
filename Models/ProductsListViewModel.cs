using FoodStore.Entities;
using System.Collections.Generic;

namespace FoodStore.Models
{
    public class ProductsListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
        public string SearchQuery { get; set; }
    }
}