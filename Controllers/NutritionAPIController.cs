using FoodStore.Infrastructure.LocalAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodStore.Controllers
{
    public class NutritionAPIController : Controller
    {
        [HttpPost]
        public ActionResult GetNutritionFoodList(string name)
        {
            var res = NutritionProvider.GetFoodList(name);
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetNutritionValues(FoodName foodName)
        {
            System.Diagnostics.Debug.WriteLine(foodName.Name);
            var res = NutritionProvider.GetNutritionValues(foodName.Name);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
    }

    public class FoodName
    {
        public string Name { get; set; }
    }
}