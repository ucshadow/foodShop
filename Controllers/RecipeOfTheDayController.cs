using FoodStore.Infrastructure.LocalAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FoodStore.Controllers
{
    public class RecipeOfTheDayController : Controller
    {
        private static string _s = "";
        private Random _rnd = new Random();
        // GET: RecipeOfTheDay
        public ActionResult Index(int refresh=0)
        {
            Debug.WriteLine(refresh);
            var json = "";
            if(_s == "" || refresh != 0) 
            {
                var a = Environment.GetEnvironmentVariable("f_app", EnvironmentVariableTarget.Machine);
                var k = Environment.GetEnvironmentVariable("f_k", EnvironmentVariableTarget.Machine);
                var q = GetQueryName();
                try
                {
                    using (var wc = new WebClient())
                    {
                        json = wc.DownloadString($"https://api.edamam.com/search?q={q}&app_id={a}&app_key={k}&from=0&to=1");
                        _s = json;
                    }
                }
                catch(Exception e)
                {
                    Debug.WriteLine(e.Message);
                    return View();
                }
            }
            ViewBag.R = _s;
            return View();
        }

        private string GetQueryName()
        {
            var safe = new string[] { "chicken", "potatoes", "tomatoes", "crab", "strawberries", "cocoa", "coffee", "tuna", "milk" };
            var repo = GlobalProductCache.ProductCache;
            if (repo == null) return safe[_rnd.Next(0, safe.Length)];
            for (var i = 0; i < 100; i++)
            {
                var p = repo.ElementAt(_rnd.Next(0, repo.Count() - 1));
                if(p.Name.Split(' ').Length < 2 && !p.Name.Contains(","))
                {
                    return p.Name;
                }
            }
            return "chicken";
        }
    }
}