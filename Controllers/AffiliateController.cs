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
using FoodStore.Infrastructure.LocalAPI;

namespace FoodStore.Controllers
{

    // all endpoints could ask for the affiliate id, but for now they are opened to anyone (except the POST one of course).
    
    public class AffiliateController : Controller
    {
        private readonly IAffiliateRepository _aRepository;
        private readonly IProductRepository _pRepository;
        private readonly IOrderProcessor _oProcessor;
        private readonly IAffiliateProductRepository _apRepository;
        private readonly IPurchaseHistoryRepository _phRepository;

        public AffiliateController(IAffiliateRepository affiliateRepository, IProductRepository productRepository, 
            IOrderProcessor orderProcessor, IAffiliateProductRepository affiliateProduct,
            IPurchaseHistoryRepository purchaseHistoryRepository)
        {
            _aRepository = affiliateRepository;
            _pRepository = productRepository;
            _oProcessor = orderProcessor;
            _apRepository = affiliateProduct;
            _phRepository = purchaseHistoryRepository;
        }

        [Authorize]
        public ActionResult Index()
        {
            var affiliate = _aRepository.GetAffiliateByUserId(User.Identity.GetUserId());

            if (affiliate != null) return View(new AffiliateModel
            {
                Affiliate = affiliate,
                AffiliateProducts = _apRepository.GetAffiliateProductsByAffiliateId(affiliate.AffiliateId).ToList(),
                AffiliateProductSales = GetAffiliateSells()
            });

            return View("~/Views/Affiliate/NotAffiliate.cshtml");
        }

        [HttpPost]
        [Authorize]
        public ActionResult BecomeAffiliate(string name)
        {
            if (name.Length > 100) name = name.Substring(0, 100);
            _aRepository.CreateAffiliate(User.Identity.GetUserId(), name);
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
            return Json(new { GlobalProductCache.ProductCache },
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

        public ActionResult EditAffiliateProduct(int affiliateProductId)
        {
            var ap = _apRepository.GetAffiliateProducts().FirstOrDefault(e => e.AffiliateProductID == affiliateProductId);
            return View(ap);
        }

        [HttpPost]
        [Authorize]
        public ActionResult EditAffiliateProduct(AffiliateProduct affiliateProduct)
        {            
            if (ModelState.IsValid)
            {

                CheckAffiliateData(affiliateProduct);

                ViewBag.Message = $"Edit {affiliateProduct.Name} success";
                _apRepository.SaveAffiliateProduct(affiliateProduct);
                return View(affiliateProduct);
            }
            ViewBag.Message = ("Something went wrong");
            return View(affiliateProduct);
        }

        public ViewResult CreateAffiliateProduct()
        {
            return View("EditAffiliateProduct", new AffiliateProduct());
        }

        [HttpPost]
        [Authorize]
        public ActionResult DeleteAffiliateProduct(int affiliateProductID)
        {
            Debug.WriteLine(affiliateProductID);    
            _apRepository.DeleteAffiliateProduct(affiliateProductID);
            return Redirect(Request.UrlReferrer.ToString());
        }

        [Authorize]
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

        private void CheckAffiliateData(AffiliateProduct affiliateProduct)
        {
            if (!string.IsNullOrEmpty(affiliateProduct.AffiliateName) && !string.IsNullOrEmpty(affiliateProduct.AffiliateId)) return;

            var affiliateUser = _aRepository.GetAffiliateByUserId(User.Identity.GetUserId());

            if (string.IsNullOrEmpty(affiliateProduct.AffiliateName))
            {
                affiliateProduct.AffiliateName = affiliateUser.AffiliateName;
            }
            if (string.IsNullOrEmpty(affiliateProduct.AffiliateId))
            {
                affiliateProduct.AffiliateId = affiliateUser.AffiliateId;
            }

        }

        private Dictionary<string, List<SellData>> GetAffiliateSells()
        {
            var res = new Dictionary<string, List<SellData>>();
            var aff = _aRepository.GetAffiliateByUserId(User.Identity.GetUserId());

            foreach(var ph in _phRepository.PurchaseHistory)
            {
                if(ph.AffiliateId == aff.AffiliateId)
                {
                    if(res.ContainsKey(ph.ProductName))
                    {
                        res[ph.ProductName].Add(new SellData 
                        { 
                            Count = ph.ProductCount, 
                            Date = ph.PurchaseDate,
                            Price = ph.Price
                        });
                    }
                    else
                    {
                        res[ph.ProductName] = new List<SellData> 
                        {
                            new SellData
                            {
                                Count = ph.ProductCount,
                                Date = ph.PurchaseDate,
                                Price = ph.Price
                            }
                        };
                    }
                }
            }
            return res;
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