using FoodStore.Entities;
using FoodStore.Infrastructure.LocalAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace FoodStore.Models
{
    public class Nutrition
    {
        private static List<Product> _clone = null;

        public Product Product { get; set; }
        
        public Dictionary<string, string[]> NutritionInfo { get; set; }

        public List<string> FoodList { get; set; }

        public string SelectedNutritionName { get; set; }

        public Nutrition(Product product)
        {
            Product = product;
            NutritionInfo = new Dictionary<string, string[]>();
            FoodList = new List<string>();
        }

        public Nutrition()
        {
            NutritionInfo = new Dictionary<string, string[]>();
            FoodList = new List<string>();
        }

        public Nutrition(string productName)
        {
            if (_clone == null) _clone = RealTimeSellData.Products;
            Product = _clone?.FirstOrDefault(e => e.Name == productName);
            NutritionInfo = new Dictionary<string, string[]>();
            FoodList = new List<string>();
        }

        public Dictionary<string, string[]> NutritionInfoProductList()
        {
            return NutritionProvider.ParseNutritionInfo(Product.Name);
        }

        public List<string> GetFoodsThatContains(string name)
        {
            return NutritionProvider.GetFoodList(name);
        }

        public static Dictionary<string, string[]> GetNutritionValues(string foodName)
        {
            return NutritionProvider.GetNutritionValues(foodName);
        }
    }
}