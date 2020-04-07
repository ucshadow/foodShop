using FoodStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodStore.Models;
using FoodStore.Infrastructure.LocalAPI;
using System.Diagnostics;
using FoodStore.Abstract;
using Microsoft.AspNet.Identity;
using FoodStore.Infrastructure.Cache;
using FoodStore.Infrastructure;

namespace FoodStore.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductRepository _repository;
        private readonly IOrderProcessor _orderProcessor;
        private static readonly Cart _cart = new Cart();

        public CartController(IProductRepository repository, IOrderProcessor processor)
        {
            _repository = repository;
            _orderProcessor = processor;
        }

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                ReturnUrl = returnUrl,
                Cart = _cart
            });
        }

        [HttpPost]
        public ActionResult AddToCart(int productId)
        {
            Product product = _repository.Products
            .FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                _cart.AddItem(product, 1);                
            }
            return Json(new { _cart }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ComputeCartValue()
        {
            return Json(new { total = _cart.ComputeTotalValue() }, JsonRequestBehavior.AllowGet);
        }

        public RedirectToRouteResult RemoveFromCart(int productId, string returnUrl)
        {
            Product product = _repository.Products
            .FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                _cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public PartialViewResult Summary()
        {
            return PartialView(_cart);
        }

        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }

        [HttpPost]
        [Authorize]
        public ViewResult Checkout(ShippingDetails shippingDetails)
        {
            if (_cart.CartEntries.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                _orderProcessor.ProcessOrder(_cart, shippingDetails, userId);

                _cart.Clear();
                return View("Completed");
            }
            return View(shippingDetails);
        }

        [HttpPost]
        public ActionResult MinusItem(int productId)
        {
            
            // check if the cart contains the item
            if(_cart.CartEntries.FirstOrDefault(e => e.Product.ProductID == productId) == null)
            {
                return Json(new { id = productId, missing = true }, JsonRequestBehavior.AllowGet);
            }

            foreach(var p in _cart.CartEntries)
            {
                if (p.Product.ProductID != productId) continue;
                if(p.Quantity == 1)
                {
                    _cart.CartEntries.Remove(p);
                    return Json(new { id = productId, missing = true }, JsonRequestBehavior.AllowGet);
                }
                p.Quantity -= 1;
            }
            return Json(new { id = productId, missing = false }, JsonRequestBehavior.AllowGet);
        }


        // casted decimals as string beacuse javascript removes the 0 at the end
        // so you end up with 10.6 instead of 10.60 on each update
        [HttpPost]
        public ActionResult GetCartTotalForCart()
        {
            // productID, totalPrice
            var pidToTotal = new Dictionary<string, string>();
            foreach(var entry in _cart.CartEntries)
            {
                pidToTotal[entry.Product.ProductID.ToString()] = ((
                    entry.Product.Discount > 0 ?
                    Helpers.CalculateDiscount(entry.Product.Price, entry.Product.Discount) :
                    entry.Product.Price) * entry.Quantity).ToString();
                
            }

            return Json(new { totalValue = _cart.ComputeTotalValue().ToString(),
                totalProducts = _cart.CartEntries.Sum(x => x.Quantity),
                pidToTotal,
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
