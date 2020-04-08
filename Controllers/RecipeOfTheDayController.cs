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
        private readonly Random _rnd = new Random();
        // GET: RecipeOfTheDay
        public ActionResult Index(int refresh=0)
        {
            Debug.WriteLine(refresh);
            var noEnvVars = "{q: \"apple\",from: 0,to: 1,more: true,count: 60161,hits: [{recipe: {uri: \"http:\\\\www.edamam.com\\ontologies\\edamam.owl#recipe_efc78b450655c26cf4819f87dab2987c\",label: \"Apple Elixir Recipe\",image: \"https:\\\\www.edamam.com\\web-img\\efe\\efe1546ab8593aaa62ed4fac11838f35.jpg\",source: \"Serious Eats\",url: \"http:\\\\www.seriouseats.com\\recipes\\2014\\09\\apple-elixir-cocktail-recipe.html\",shareAs: \"http:\\\\www.edamam.com\\recipe\\apple-elixir-recipe-efc78b450655c26cf4819f87dab2987c\\apple\",yield: 4,dietLabels: [\"Low-Fat\"],healthLabels: [\"Vegan\",\"Vegetarian\",\"Peanut-Free\",\"Tree-Nut-Free\"],cautions: [\"Sulfites\"],ingredientLines: [\"For the Spiced Cider Concentrate:\",\"4 cups fresh apple cider\",\"2 cinnamon sticks\",\"4 allspice berries, whole\",\"1 teaspoon cloves, whole\",\"2 teaspoons black peppercorns, whole\",\"For the Cocktail:\",\"2 ounces Laird's Bonded Apple Brandy\",\"1 ounce apple cider concentrate syrup\",\"1\\2 ounce freshly squeezed lemon juice from about half a lemon\",\"4 ounces hard cider such as Samuel Smith\u2019s Organic Cider\",\"Apple slice or apple chip for garnish (optional)\"],ingredients: [{text: \"4 cups fresh apple cider\",weight: 992},{text: \"2 cinnamon sticks\",weight: 5.2},{text: \"4 allspice berries, whole\",weight: 7.6},{text: \"1 teaspoon cloves, whole\",weight: 2.1},{text: \"2 teaspoons black peppercorns, whole\",weight: 5.8},{text: \"2 ounces Laird's Bonded Apple Brandy\",weight: 56.69904625},{text: \"1 ounce apple cider concentrate syrup\",weight: 28.349523125},{text: \"1\\2 ounce freshly squeezed lemon juice from about half a lemon\",weight: 14.1747615625},{text: \"4 ounces hard cider such as Samuel Smith\u2019s Organic Cider\",weight: 113.3980925}],calories: 769.4291270562501,totalWeight: 1225.3214234375,totalTime: 120,totalNutrients: {ENERC_KCAL: {label: \"Energy\",quantity: 769.4291270562501,unit: \"kcal\"},FAT: {label: \"Fat\",quantity: 2.675046661875,unit: \"g\"},FASAT: {label: \"Saturated\",quantity: 0.62630995159375,unit: \"g\"},FATRN: {label: \"Trans\",quantity: 0.005334,unit: \"g\"},FAMS: {label: \"Monounsaturated\",quantity: 0.20535981878750004,unit: \"g\"},FAPU: {label: \"Polyunsaturated\",quantity: 0.755407374934375,unit: \"g\"},CHOCDF: {label: \"Carbs\",quantity: 159.65059330331252,unit: \"g\"},FIBTG: {label: \"Fiber\",quantity: 8.8354204696875,unit: \"g\"},SUGAR: {label: \"Sugars\",quantity: 124.03656217125001,unit: \"g\"},SUGAR.added: {label: \"Sugars, added\",quantity: 17.140121681375003,unit: \"g\"},PROCNT: {label: \"Protein\",quantity: 2.56465956721875,unit: \"g\"},CHOLE: {label: \"Cholesterol\",quantity: 0,unit: \"mg\"},NA: {label: \"Sodium\",quantity: 61.675604553125,unit: \"mg\"},CA: {label: \"Calcium\",quantity: 259.50484668124994,unit: \"mg\"},MG: {label: \"Magnesium\",quantity: 90.810790175,unit: \"mg\"},K: {label: \"Potassium\",quantity: 1392.545047784375,unit: \"mg\"},FE: {label: \"Iron\",quantity: 3.1724916141875004,unit: \"mg\"},ZN: {label: \"Zinc\",quantity: 0.9572446077187501,unit: \"mg\"},P: {label: \"Phosphorus\",quantity: 104.61079971250001,unit: \"mg\"},VITA_RAE: {label: \"Vitamin A\",quantity: 4.566,unit: \"\u00B5g\"},VITC: {label: \"Vitamin C\",quantity: 18.615215557187504,unit: \"mg\"},THIA: {label: \"Thiamin (B1)\",quantity: 0.2760501702375,unit: \"mg\"},RIBF: {label: \"Riboflavin (B2)\",quantity: 0.5743307954968752,unit: \"mg\"},NIA: {label: \"Niacin (B3)\",quantity: 1.2358516302906248,unit: \"mg\"},VITB6A: {label: \"Vitamin B6\",quantity: 0.25589102789375,unit: \"mg\"},FOLDFE: {label: \"Folate equivalent (total)\",quantity: 7.393952312500001,unit: \"\u00B5g\"},FOLFD: {label: \"Folate (food)\",quantity: 7.393952312500001,unit: \"\u00B5g\"},FOLAC: {label: \"Folic acid\",quantity: 0,unit: \"\u00B5g\"},VITB12: {label: \"Vitamin B12\",quantity: 0,unit: \"\u00B5g\"},VITD: {label: \"Vitamin D\",quantity: 0,unit: \"IU\"},TOCPHA: {label: \"Vitamin E\",quantity: 0.49798195159374997,unit: \"mg\"},VITK1: {label: \"Vitamin K\",quantity: 14.0948,unit: \"\u00B5g\"},WATER: {label: \"Water\",quantity: 1037.555044563031,unit: \"g\"}},totalDaily: {ENERC_KCAL: {label: \"Energy\",quantity: 38.47145635281251,unit: \"%\"},FAT: {label: \"Fat\",quantity: 4.1154564028846154,unit: \"%\"},FASAT: {label: \"Saturated\",quantity: 3.13154975796875,unit: \"%\"},CHOCDF: {label: \"Carbs\",quantity: 53.21686443443751,unit: \"%\"},FIBTG: {label: \"Fiber\",quantity: 35.34168187875,unit: \"%\"},PROCNT: {label: \"Protein\",quantity: 5.1293191344375,unit: \"%\"},CHOLE: {label: \"Cholesterol\",quantity: 0,unit: \"%\"},NA: {label: \"Sodium\",quantity: 2.569816856380208,unit: \"%\"},CA: {label: \"Calcium\",quantity: 25.950484668124993,unit: \"%\"},MG: {label: \"Magnesium\",quantity: 21.621616708333335,unit: \"%\"},K: {label: \"Potassium\",quantity: 29.628618037965424,unit: \"%\"},FE: {label: \"Iron\",quantity: 17.624953412152777,unit: \"%\"},ZN: {label: \"Zinc\",quantity: 8.702223706534092,unit: \"%\"},P: {label: \"Phosphorus\",quantity: 14.944399958928573,unit: \"%\"},VITA_RAE: {label: \"Vitamin A\",quantity: 0.5073333333333333,unit: \"%\"},VITC: {label: \"Vitamin C\",quantity: 20.68357284131945,unit: \"%\"},THIA: {label: \"Thiamin (B1)\",quantity: 23.004180853125003,unit: \"%\"},RIBF: {label: \"Riboflavin (B2)\",quantity: 44.179291961298084,unit: \"%\"},NIA: {label: \"Niacin (B3)\",quantity: 7.724072689316405,unit: \"%\"},VITB6A: {label: \"Vitamin B6\",quantity: 19.683925222596155,unit: \"%\"},FOLDFE: {label: \"Folate equivalent (total)\",quantity: 1.8484880781250002,unit: \"%\"},VITB12: {label: \"Vitamin B12\",quantity: 0,unit: \"%\"},VITD: {label: \"Vitamin D\",quantity: 0,unit: \"%\"},TOCPHA: {label: \"Vitamin E\",quantity: 3.3198796772916666,unit: \"%\"},VITK1: {label: \"Vitamin K\",quantity: 11.745666666666667,unit: \"%\"}},digest: [{label: \"Fat\",tag: \"FAT\",schemaOrgTag: \"fatContent\",total: 2.675046661875,hasRDI: true,daily: 4.1154564028846154,unit: \"g\",sub: [{label: \"Saturated\",tag: \"FASAT\",schemaOrgTag: \"saturatedFatContent\",total: 0.62630995159375,hasRDI: true,daily: 3.13154975796875,unit: \"g\"},{label: \"Trans\",tag: \"FATRN\",schemaOrgTag: \"transFatContent\",total: 0.005334,hasRDI: false,daily: 0,unit: \"g\"},{label: \"Monounsaturated\",tag: \"FAMS\",schemaOrgTag: null,total: 0.20535981878750004,hasRDI: false,daily: 0,unit: \"g\"},{label: \"Polyunsaturated\",tag: \"FAPU\",schemaOrgTag: null,total: 0.755407374934375,hasRDI: false,daily: 0,unit: \"g\"}]},{label: \"Carbs\",tag: \"CHOCDF\",schemaOrgTag: \"carbohydrateContent\",total: 159.65059330331252,hasRDI: true,daily: 53.21686443443751,unit: \"g\",sub: [{label: \"Carbs (net)\",tag: \"CHOCDF.net\",schemaOrgTag: null,total: 150.815172833625,hasRDI: false,daily: 0,unit: \"g\"},{label: \"Fiber\",tag: \"FIBTG\",schemaOrgTag: \"fiberContent\",total: 8.8354204696875,hasRDI: true,daily: 35.34168187875,unit: \"g\"},{label: \"Sugars\",tag: \"SUGAR\",schemaOrgTag: \"sugarContent\",total: 124.03656217125001,hasRDI: false,daily: 0,unit: \"g\"},{label: \"Sugars, added\",tag: \"SUGAR.added\",schemaOrgTag: null,total: 17.140121681375003,hasRDI: false,daily: 0,unit: \"g\"}]},{label: \"Protein\",tag: \"PROCNT\",schemaOrgTag: \"proteinContent\",total: 2.56465956721875,hasRDI: true,daily: 5.1293191344375,unit: \"g\"},{label: \"Cholesterol\",tag: \"CHOLE\",schemaOrgTag: \"cholesterolContent\",total: 0,hasRDI: true,daily: 0,unit: \"mg\"},{label: \"Sodium\",tag: \"NA\",schemaOrgTag: \"sodiumContent\",total: 61.675604553125,hasRDI: true,daily: 2.569816856380208,unit: \"mg\"},{label: \"Calcium\",tag: \"CA\",schemaOrgTag: null,total: 259.50484668124994,hasRDI: true,daily: 25.950484668124993,unit: \"mg\"},{label: \"Magnesium\",tag: \"MG\",schemaOrgTag: null,total: 90.810790175,hasRDI: true,daily: 21.621616708333335,unit: \"mg\"},{label: \"Potassium\",tag: \"K\",schemaOrgTag: null,total: 1392.545047784375,hasRDI: true,daily: 29.628618037965424,unit: \"mg\"},{label: \"Iron\",tag: \"FE\",schemaOrgTag: null,total: 3.1724916141875004,hasRDI: true,daily: 17.624953412152777,unit: \"mg\"},{label: \"Zinc\",tag: \"ZN\",schemaOrgTag: null,total: 0.9572446077187501,hasRDI: true,daily: 8.702223706534092,unit: \"mg\"},{label: \"Phosphorus\",tag: \"P\",schemaOrgTag: null,total: 104.61079971250001,hasRDI: true,daily: 14.944399958928573,unit: \"mg\"},{label: \"Vitamin A\",tag: \"VITA_RAE\",schemaOrgTag: null,total: 4.566,hasRDI: true,daily: 0.5073333333333333,unit: \"\u00B5g\"},{label: \"Vitamin C\",tag: \"VITC\",schemaOrgTag: null,total: 18.615215557187504,hasRDI: true,daily: 20.68357284131945,unit: \"mg\"},{label: \"Thiamin (B1)\",tag: \"THIA\",schemaOrgTag: null,total: 0.2760501702375,hasRDI: true,daily: 23.004180853125003,unit: \"mg\"},{label: \"Riboflavin (B2)\",tag: \"RIBF\",schemaOrgTag: null,total: 0.5743307954968752,hasRDI: true,daily: 44.179291961298084,unit: \"mg\"},{label: \"Niacin (B3)\",tag: \"NIA\",schemaOrgTag: null,total: 1.2358516302906248,hasRDI: true,daily: 7.724072689316405,unit: \"mg\"},{label: \"Vitamin B6\",tag: \"VITB6A\",schemaOrgTag: null,total: 0.25589102789375,hasRDI: true,daily: 19.683925222596155,unit: \"mg\"},{label: \"Folate equivalent (total)\",tag: \"FOLDFE\",schemaOrgTag: null,total: 7.393952312500001,hasRDI: true,daily: 1.8484880781250002,unit: \"\u00B5g\"},{label: \"Folate (food)\",tag: \"FOLFD\",schemaOrgTag: null,total: 7.393952312500001,hasRDI: false,daily: 0,unit: \"\u00B5g\"},{label: \"Folic acid\",tag: \"FOLAC\",schemaOrgTag: null,total: 0,hasRDI: false,daily: 0,unit: \"\u00B5g\"},{label: \"Vitamin B12\",tag: \"VITB12\",schemaOrgTag: null,total: 0,hasRDI: true,daily: 0,unit: \"\u00B5g\"},{label: \"Vitamin D\",tag: \"VITD\",schemaOrgTag: null,total: 0,hasRDI: true,daily: 0,unit: \"\u00B5g\"},{label: \"Vitamin E\",tag: \"TOCPHA\",schemaOrgTag: null,total: 0.49798195159374997,hasRDI: true,daily: 3.3198796772916666,unit: \"mg\"},{label: \"Vitamin K\",tag: \"VITK1\",schemaOrgTag: null,total: 14.0948,hasRDI: true,daily: 11.745666666666667,unit: \"\u00B5g\"},{label: \"Sugar alcohols\",tag: \"Sugar.alcohol\",schemaOrgTag: null,total: 0,hasRDI: false,daily: 0,unit: \"g\"},{label: \"Water\",tag: \"WATER\",schemaOrgTag: null,total: 1037.555044563031,hasRDI: false,daily: 0,unit: \"g\"}]},bookmarked: false,bought: false}]}";
            if(_s == "" || refresh != 0) 
            {
                var a = Environment.GetEnvironmentVariable("f_app", EnvironmentVariableTarget.Process);
                var k = Environment.GetEnvironmentVariable("f_k", EnvironmentVariableTarget.Process);

                if(string.IsNullOrEmpty(a) || string.IsNullOrEmpty(k))
                {
                    _s = noEnvVars;
                    return View();
                }

                var q = GetQueryName();
                try
                {
                    using (var wc = new WebClient())
                    {
                        _s = wc.DownloadString($"https://api.edamam.com/search?q={q}&app_id={a}&app_key={k}&from=0&to=1");
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