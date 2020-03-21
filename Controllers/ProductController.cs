using FoodStore.Abstract;
using FoodStore.Entities;
using FoodStore.Infrastructure.LocalAPI;
using FoodStore.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace FoodStore.Controllers
{
    public class ProductController : Controller
    {


        public readonly IProductRepository Repository;
        public int PageSize = 8;
        public readonly Random Rnd = new Random();

        public ProductController(IProductRepository productRepository)
        {
            Repository = productRepository;
            RealTimeSellData.Products = productRepository.GetClone();
            new RealTimeSellData().Loop();
        }

        public ViewResult List(string category, int page = 1, string q = "")
        {
            IEnumerable<Product> products = null;
            var searchReultsCount = 0;

            if(q == null || q.Trim().Length == 0)
            {
                products = Repository.Products
                .Where(p => category == null || p.Category == category)
                .OrderBy(p => category == null ? Rnd.Next() : p.ProductID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize);
            }
            else
            {   // search
                var parsed = ParseNameForSearch(q.ToLower());
                var x = new HashSet<string>();
                foreach(var i in parsed.Split(' '))
                {
                    x.Add(i);
                }
                var tempProducts = Repository.Products
                //.Where(p => p.Name.ToLower().Contains(parsed))
                .Where(p => p.Name.ToLower().Split(' ').ToHashSet().Intersect(x).Count() > 0);

                searchReultsCount = tempProducts.Count();

                products = tempProducts.Skip((page - 1) * PageSize)
                .Take(PageSize);


            }

            Debug.WriteLine("Product count: " + searchReultsCount);

            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = products,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = searchReultsCount == 0 ? TotalItems(category, q) : searchReultsCount
                },
                CurrentCategory = category,
                SearchQuery = q
            };
            return View(model);
        }

        private string ParseNameForSearch(string n)
        {
            var o = "";
            var res = new List<string>();

            var lolMurica = new string[] 
            { "tablespoon", "cup", "teaspoon", "dash", "pinch", "drop", "ounce", "pound", "pint", "fluid",
            "quart", "gallon", "peck", "gram", "sweetened", "unsweetened"};

            var alpha = "abcdefghijklmnopqrstuvwxyz ";            

            // remove paranthesis
            var regex = "(\\[.*\\])|(\".*\")|('.*')|(\\(.*\\))";
            _ = Regex.Replace(n, regex, "");



            foreach (var s in lolMurica)
            {
                n = n.Replace($"a {s}", "").Replace($"{s}s", "").Replace(s, "");
            }

            foreach(var c in n) 
            {
                if(alpha.Contains(c))
                {
                    o += c;
                }
            }
            

            // remove the s at the end of words

            var wList = o.Split(' ');
            foreach(var i in wList)
            {
                if (i.EndsWith("s"))
                {
                   res.Add(i.Remove(i.Length - 1).Trim());
                } else
                {
                    res.Add(i.Trim());
                }
            }

            Debug.WriteLine(n + " ==> " + string.Join(" ", res));
            return string.Join(" ", res);
            
        }

        [Route("/Details/{product?}")]
        public ViewResult Details(string productName)
        {
            var x = Repository.Products.FirstOrDefault(e => e.Name == productName);
            // this may be null..
            var moreFromCategory = Repository.Products.Where(e => e.Category == x.Category).Take(4).ToList();
            ViewBag.More = moreFromCategory;            
            return View(x);
        }

        private int TotalItems(string category, string q)
        {
            if(category == null || category.Trim().Length == 0)
            {
                return 0; // this will only hit the first page where no page count is shown
            }
            if(category == "Search Results")
            {
                return Repository.Products
                    .Where(p => p.Name.ToLower().Contains(q.ToLower()))
                    .Count();
            }
            return Repository.Products.Where(e => e.Category == category).Count();
        }

    }
}