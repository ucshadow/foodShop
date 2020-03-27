using FoodStore.Abstract;
using FoodStore.Entities;
using FoodStore.Infrastructure;
using FoodStore.Infrastructure.Cache;
using FoodStore.Infrastructure.Discounts;
using FoodStore.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodStore.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IProductRepository _pRepository;
        public AdminController(IProductRepository repo)
        {
            _pRepository = repo;
        }
        public ViewResult Index()
        {
            
            return View();
        }

        public ViewResult Cache()
        {
            // since Global Cache is singleton, the model can safely be instatiated as many times as wanted
            return View(new AdminCacheModel());
        }

        public ViewResult Discounts()
        {
            var a = new AdminDiscountsModel();
            return View(a);
        }

        public ViewResult Edit(int productId)
        {
            Product product = _pRepository.Products
            .FirstOrDefault(p => p.ProductID == productId);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            // todo also update cache :)
            if (ModelState.IsValid)
            {
                _pRepository.SaveProduct(product);
                CheckIfDiscountChanged(product);
                TempData["message"] = string.Format("{0} has been saved", product.Name);
                ViewBag.Message = $"Edit {product.Name} success";
                return View(product);
            }
            else
            {
                // there is something wrong with the data values
                ViewBag.Message = ("Something went wrong");
                return View(product);
            }
        }

        public ViewResult Create()
        {
            return View("Edit", new Product());
        }

        [HttpPost]
        public ActionResult Delete(int productId)
        {
            Product deletedProduct = _pRepository.DeleteProduct(productId);
            if (deletedProduct != null)
            {
                TempData["message"] = string.Format("{0} was deleted",
                deletedProduct.Name);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DiscountCategory(string category, decimal discount)
        {
            if (discount > 100 || discount < 0)
            {
                return RedirectToAction("List", "Product", new { category, message = $"discount must be between 0 and 100 -> {discount}" });
            }

            var productsFormCat = _pRepository.Products.Where(e => e.Category == category).ToList();
            if(productsFormCat == null || productsFormCat.Count() == 0)
            {
                return RedirectToAction("List", "Product", new { category, message = $"No products with category {category}" });
            }

            

            foreach(var p in productsFormCat)
            {
                p.Discount = discount;
                p.Price = Helpers.CalculateDiscount(p.Price, p.Discount);
                _pRepository.SaveProduct(p);
            }

            return RedirectToAction("List", "Product", new { category, message = $"Succes applying {discount} discount to {category} category" });
         
        }


        [HttpPost]
        public ActionResult ClearCache(string type)
        {
            var clearSuccess = GlobalCache.GetCache().ClearCache(type);
            ViewBag.Message = clearSuccess ? $"clearing cash for {type} success" : $"clearing cash for {type} failed";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult RemoveItemFromDiscount(int productID)
        {
            var product = _pRepository.Products.FirstOrDefault(e => e.ProductID == productID);

            if(product == null) return RedirectToAction("Cache"); ;

            product.Discount = 0;
            DiscountProvider.DiscountsManager.RemoveProductIfExists(productID);
            _pRepository.SaveProduct(product);

            return RedirectToAction("Discounts");
        }

        [HttpPost]
        public ActionResult SetMaxDiscountPercentage(int? n)
        {
            if (n == null) n = 0;
            DiscountProvider.DiscountsManager.SetMaxDiscountPercentage(n);
            return RedirectToAction("Discounts");
        }

        

        [HttpPost]
        public ActionResult SetMaxProductsDiscounted(int? n)
        {
            if (n == null) n = 0;
            DiscountProvider.DiscountsManager.SetMaxProductsDiscounted(n);
            return RedirectToAction("Discounts");
        }

        [HttpPost]
        public ActionResult SetCheapBias(int? n)
        {
            if (n == null) n = 0;
            DiscountProvider.DiscountsManager.SetCheapBias(n);
            return RedirectToAction("Discounts");
        }

        [HttpPost]
        public ActionResult SetUpdateTimeInSeconds(int? n)
        {
            if (n == null) n = 0;
            DiscountProvider.DiscountsManager.SetUpdateTimeInSeconds(n);
            return RedirectToAction("Discounts");
        }

        [HttpPost]
        public ActionResult TriggerManualUpdate()
        {
            DiscountProvider.DiscountsManager.TriggerManualUpdate();
            return RedirectToAction("Discounts");
        }

        [HttpPost]
        public ActionResult AddToDailyDiscount(int productID)
        {
            var p = _pRepository.Products.FirstOrDefault(e => e.ProductID == productID);
            if(p != null)
            {
                DiscountProvider.DiscountsManager.AddItem(p);
                DiscountProvider.DiscountsManager.SetMaxProductsDiscounted(DiscountProvider.DiscountsManager.GetMaxProductsDiscounted() + 1);

            }
            return Redirect(Request.UrlReferrer.ToString());
        }

        private void CheckIfDiscountChanged(Product product)
        {
            if(DiscountProvider.DiscountsManager.ProductExists(product.ProductID))
            {
                if(product.Discount == 0)
                {
                    DiscountProvider.DiscountsManager.RemoveProductIfExists(product.ProductID);
                }
                else
                {
                    DiscountProvider.DiscountsManager.RemoveProductIfExists(product.ProductID);
                    DiscountProvider.DiscountsManager.AddItem(product, false);
                }
            }
        }
    }
}