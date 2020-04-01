using FoodStore.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodStore.HtmlHelpers;
using FoodStore.Entities;
using FoodStore.Models;
using FoodStore.Infrastructure;
using System.Diagnostics;

namespace FoodStore.Controllers
{

    // all endpoints could ask for the affiliate id, but for now they are opened to anyone (except the POST one of course).
    
    public class AffiliateController : Controller
    {
        private readonly IAffiliateRepository _aRepository;
        private readonly IProductRepository _pRepository;
        private readonly IOrderProcessor _oProcessor;

        public AffiliateController(IAffiliateRepository affiliateRepository, IProductRepository productRepository, 
            IOrderProcessor orderProcessor)
        {
            _aRepository = affiliateRepository;
            _pRepository = productRepository;
            _oProcessor = orderProcessor;
        }

        [Authorize]
        public ActionResult Index()
        {
            var affiliate = _aRepository.GetAffiliateByUserId(User.Identity.GetUserId());

            if(affiliate != null) return View(new AffiliateModel { Affiliate = affiliate });

            return View("~/Views/Affiliate/NotAffiliate.cshtml");
        }

        [HttpPost]
        [Authorize]
        public ActionResult BecomeAffiliate()
        {
            _aRepository.CreateAffiliate(User.Identity.GetUserId());
            return Redirect(Request.UrlReferrer.ToString());
        }

        [Cors]
        public ActionResult SellItem(Details details) {
            ProcessAffiliateOrder(details.Product, details.ShippingDetails, details.Quantity, details.AffiliateId);
            return Json(new { Res = "OK" });
        }

        [Cors]
        public ActionResult GetProductById(int productId)
        {
            return Json(new { Product = _pRepository.Products.FirstOrDefault(e => e.ProductID == productId) },
                JsonRequestBehavior.AllowGet);
        }

        [Cors]
        public ActionResult GetAllProducts()
        {
            return Json(new { _pRepository.Products },
                JsonRequestBehavior.AllowGet);
        }

        [Cors]
        public ActionResult GetShippingAddressForm()
        {
            return Json(new { AddressForm = new ShippingDetails 
            { 
                Name = "Required",
                Country = "Required",
                Line1 = "Required"
            } },
                JsonRequestBehavior.AllowGet);
        }

        public ActionResult Example()
        {
            var affiliate = _aRepository.GetAffiliateByUserId(User.Identity.GetUserId());
            if (affiliate != null) ViewBag.AffiliateId = affiliate.AffiliateId;
            return View();
        }

        private void ProcessAffiliateOrder(Product product, ShippingDetails shippingDetails, int quantity, string affiliateId)
        {
            for(var i = 0; i < quantity; i++)
            {
                _oProcessor.ProcessAffiliateOrder(product, shippingDetails, affiliateId);
            }
            _aRepository.AddAffiliateSell(product, shippingDetails, quantity, affiliateId);

        }

        public class Details
        {
            public Product Product { get; set; }
            public ShippingDetails ShippingDetails { get; set; }
            public int Quantity { get; set; }
            public string AffiliateId { get; set; }
        } 

    }
}