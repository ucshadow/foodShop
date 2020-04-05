using FoodStore.Entities;
using FoodStore.Infrastructure.Online;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodStore.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class ProductImporterController : Controller
    {

        private IProductImporter _productImporter;

        public ViewResult Pick(string picked)
        {

            if (picked == "skagenfood.dk") _productImporter = new Skagenfood();
            if (picked == "coop.dk") _productImporter = new Coop();
            if (picked == "coop.dk") _productImporter = new Coop();
            if (picked == "nemlig.com") _productImporter = new Nemlig();

            return View(_productImporter.GetProducts());
        }

        [HttpPost]        
        public ViewResult EditExternal(Product product)
        {
            ViewBag.Message = "Editing external product";
            return View("~/Views/Admin/Edit.cshtml", product);
        }
    }
}