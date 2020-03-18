using FoodStore.Models;
using FoodStore.Infrastructure.LocalAPI;
using System.Web.Mvc;
using System.Diagnostics;

namespace FoodStore.Controllers
{
    public class NutritionController : Controller
    {
        // GET: Nutrition
        [Route("/Nutrition/{product?}")]
        public ActionResult Index(string product)
        {
            return View(new Nutrition(product));
        }        
    }
}