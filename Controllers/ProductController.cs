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
using FoodStore.HtmlHelpers;
using FoodStore.Infrastructure.Cache;

namespace FoodStore.Controllers
{
    public class ProductController : Controller
    {


        public readonly IProductRepository Repository;
        private readonly IPurchaseHistoryRepository _purchaseHistoryRepository;
        private readonly ICommentsRepository _commentsRepository;
        private readonly IPublicProfilesRepository _ppRepository;
        public int PageSize = 8;
        public readonly Random Rnd = new Random();

        public ProductController(IProductRepository productRepository, IPublicProfilesRepository ppRepository,
            IPurchaseHistoryRepository pRepository, ICommentsRepository commentsRepository)
        {
            Repository = productRepository;
            _purchaseHistoryRepository = pRepository;
            _commentsRepository = commentsRepository;
            _ppRepository = ppRepository;
        }

        public ViewResult List(string category, int page = 1, string q = "", string message = "")
        {
            Debug.WriteLine(category);
            IEnumerable<Product> products = null;
            var searchReultsCount = 0;
            ViewBag.Message = message;

            if(q == null || q.Trim().Length == 0)
            {
                products = Repository.Products
                .Where(p => category == null || p.Category == category)
                .OrderBy(p => category == null ? Rnd.Next() : p.ProductID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize).ToList();
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
                .Where(p => p.Name.ToLower().Split(' ').ToHashSet().Intersect(x).Count() > 0);

                searchReultsCount = tempProducts.Count();

                products = tempProducts.Skip((page - 1) * PageSize)
                .Take(PageSize).ToList();


            }

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
            return View("~/Views/Product/List.cshtml", model);
        }

        [HttpPost]
        public ActionResult GetSearchData(FoodName foodName)
        {
            var parsed = ParseNameForSearch(foodName.Name.ToLower());           
            var res = GlobalProductCache.ProductCache
                .Where(p => p.Name.ToLower().Contains(parsed));

            return Json(res, JsonRequestBehavior.AllowGet);
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
            // heavy db calls ahead, maybe it could be simplified using relations?

            // or cache :)

            if (!(GlobalCache.GetCache().GetCachedItem<ProductModel>(productName) is ProductModel cached))
            {
                var p = Repository.Products.FirstOrDefault(e => e.Name == productName);
                var productDisplayed = new ProductModel
                {
                    Product = p,
                    Related = Repository.Products.Where(e => e.Category == p.Category).OrderBy(e => Rnd.Next()).Take(4).ToList(),
                    IsCommentAllowedForCurrentUser = IsAllowedToComment(p),

                    OldUserComments = _commentsRepository.GetUserComments(User.Identity.GetUserId()).ToList(),
                };
                var allCommentsForThisProduct = _commentsRepository.GetProductComments(p.ProductID);
                foreach (var comment in allCommentsForThisProduct)
                {
                    var purchase = _purchaseHistoryRepository.PurchaseHistory.FirstOrDefault(e => e.UserId == comment.AspNetUserId && e.ProductId == p.ProductID);
                    productDisplayed.Comments.Add(new CommentUserModel
                    {
                        Comment = comment,
                        User = _ppRepository.GetCommentPublicProfile(comment.AspNetUserId),
                        Rating = purchase?.Rating,
                        PurchasedOn = purchase?.PurchaseDate
                    });
                }
                
                // should update the right to comment in the cahced entry at checkout, but in theory is the same.
                GlobalCache.GetCache().CacheItem<ProductModel>(productDisplayed);
                return View(productDisplayed);
            }
            cached.IsCommentAllowedForCurrentUser = IsAllowedToComment(cached.Product);
            return View(cached);
        }

        private bool IsAllowedToComment(Product p)
        {
            // only one comment per product, should add edit comment in the future
            var hasPurchesThisProduct = _purchaseHistoryRepository.PurchaseHistory
                .FirstOrDefault(e => e.ProductId == p.ProductID && e.UserId == User.Identity.GetUserId()) != null;

            var hasCommentedYet = _commentsRepository.GetUserComments(User.Identity.GetUserId()).FirstOrDefault(e => e.ProductID == p.ProductID) != null;

            return hasPurchesThisProduct && !hasCommentedYet;
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