using FoodStore.Infrastructure.LocalAPI;
using System.Web.Mvc;

namespace FoodStore.Controllers
{
    public class RealTimeSellDataController : Controller
    {

        [HttpPost]
        public ActionResult GetSellData()
        {
            var res = RealTimeSellData.P;
            return Json(res, JsonRequestBehavior.AllowGet);
        }

    }

    
}