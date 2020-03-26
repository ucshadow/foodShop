using FoodStore.Models;
using FoodStore.Infrastructure.LocalAPI;
using System.Web.Mvc;
using System.Diagnostics;
using FoodStore.Abstract;
using System.Collections.Generic;
using FoodStore.Entities;

namespace FoodStore.Controllers
{
    public class NutritionController : Controller
    {
        // GET: Nutrition
        [Route("/Nutrition/{product?}")]
        public ViewResult Nutrition(string productName)
        {
            return View(new Nutrition(productName));
        }        
    }
}