using FoodStore.Infrastructure.LocalAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace FoodStore.Entities
{
    public class Nutrition
    {
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
    }
}