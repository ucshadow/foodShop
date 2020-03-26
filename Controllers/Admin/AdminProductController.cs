using FoodStore.Abstract;
using FoodStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodStore.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class AdminProductController : Controller
    {

        private readonly IProductRepository _pRepository;

        public AdminProductController(IProductRepository repo)
        {
            _pRepository = repo;
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
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _pRepository.SaveProduct(product);
                TempData["message"] = string.Format("{0} has been saved", product.Name);
                return RedirectToAction("Index");
            }
            else
            {
                // there is something wrong with the data values
                ViewBag.Message = ("Something went wrong");
                return View(product);
            }
        }
    }
}