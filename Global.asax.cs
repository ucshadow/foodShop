using FoodStore.Entities;
using FoodStore.WebUI.Infrastructure.Binders;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace FoodStore
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // register the CartModelBinder
            ModelBinders.Binders.Add(typeof(Cart), new CartModelBinder());
        }
    }
}
