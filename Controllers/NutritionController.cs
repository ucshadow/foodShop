using FoodStore.Infrastructure.LocalAPI;
using System.Web.Mvc;

namespace FoodStore.Controllers
{
    public class NutritionController : Controller
    {
        // GET: Nutrition
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetNutritionFoodList(string name)
        {
            var res = NutritionProvider.GetFoodList(name);
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ParseNutritionInfo(string name)
        {
            var res = NutritionProvider.ParseNutritionInfo(name);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
    }
}