using FoodStore.Infrastructure.LocalAPI;
using System.Web.Mvc;

namespace FoodStore.Controllers
{
    public class RealTimeSellDataController : Controller
    {

        [HttpPost]
        public ActionResult GetSellData()
        {
            var res = RealTimeSellData._p;
            return Json(res, JsonRequestBehavior.AllowGet);
        }

    }

    
}