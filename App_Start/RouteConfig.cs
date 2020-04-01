using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FoodStore
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(null,
                "",
                new
                {
                    controller = "Product",
                    action = "List",
                    category = (string)null,
                    page = 1
                }
            );

            routes.MapRoute(null,
                "Page{page}",
                new { controller = "Product", action = "List", category = (string)null },
                new { page = @"\d+" }
            );

            routes.MapRoute(
                "Recipe of the day",
                "recipe",
                new { controller = "RecipeOfTheDay", action = "Index"}
            );

            routes.MapRoute(null,
                "Nutrition/{product}",
                new { controller = "Nutrition", action = "Nutrition" }
            );

            routes.MapRoute(null,
                "Details/{product}",
                new { controller = "Product", action = "Details" }
            );

            routes.MapRoute(null,
                "profiles/{name}",
                new { controller = "PublicProfile", action = "GetPublicProfile" }
            );

            routes.MapRoute(null,
                "Category/{category}",
                new { controller = "Product", action = "List", page = 1 }
            );

            routes.MapRoute(null,
                "Category/{category}/Page{page}",
                new { controller = "Product", action = "List" },
                new { page = @"\d+" }
            );

            routes.MapRoute("Edit public profile",
                "edit/publicProfile",
                new { controller = "PublicProfile", action = "Index" });

            routes.MapRoute("Affiliate example",
                "affiliateExample",
                new { controller = "Affiliate", action = "Example" });

            routes.MapRoute(null, "{controller}/{action}");
        }
    }
}
