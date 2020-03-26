using FoodStore.Abstract;
using FoodStore.Entities;
using FoodStore.Infrastructure;
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
        private readonly IProductRepository _repository;
        public AdminController(IProductRepository repo)
        {
            _repository = repo;
        }
        public ViewResult Index()
        {
            return View();
        }

        public ViewResult Edit(int productId)
        {
            Product product = _repository.Products
            .FirstOrDefault(p => p.ProductID == productId);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _repository.SaveProduct(product);
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
            Product deletedProduct = _repository.DeleteProduct(productId);
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

            var productsFormCat = _repository.Products.Where(e => e.Category == category).ToList();
            if(productsFormCat == null || productsFormCat.Count() == 0)
            {
                return RedirectToAction("List", "Product", new { category, message = $"No products with category {category}" });
            }

            

            foreach(var p in productsFormCat)
            {
                p.Discount = discount;
                p.Price = Helpers.CalculateDiscount(p.Price, p.Discount);
                _repository.SaveProduct(p);
            }

            return RedirectToAction("List", "Product", new { category, message = $"Succes applying {discount} discount to {category} category" });
         
        }
    }
}