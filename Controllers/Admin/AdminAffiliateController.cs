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
    public class AdminAffiliateController : Controller
    {
        private readonly IAffiliateProductRepository _apRepository;
        private readonly IProductRepository _pRepository;

        public AdminAffiliateController(IAffiliateProductRepository affiliateProductRepository,
            IProductRepository productRepository)
        {
            _apRepository = affiliateProductRepository;
            _pRepository = productRepository;
        }

        [HttpPost]
        public ActionResult Deny(int affiliateProductId)
        {
            var ap = _apRepository.DenyAffiliateProduct(affiliateProductId);
            return Redirect(Request.UrlReferrer.ToString());
        }

        [HttpPost]
        public ActionResult Approve(int affiliateProductId)
        {
            _apRepository.ApproveAffiliateProduct(affiliateProductId);
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}