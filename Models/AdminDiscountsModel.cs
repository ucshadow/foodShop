using FoodStore.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using System.Reflection;
using FoodStore.Entities;
using System.Web.Mvc;

namespace FoodStore.Models
{
    public class AdminDiscountsModel
    {
        private IProductRepository _pRepository;

        public AdminDiscountsModel()
        {
            _pRepository = DependencyResolver.Current.GetService<IProductRepository>();
        }

        public List<Product> GetAllDiscountedItems()
        {
            return _pRepository.Products.Where(e => e.Discount > 0).ToList();
        }
    }
}