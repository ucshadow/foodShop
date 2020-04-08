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

        public AdminAffiliateController(IAffiliateProductRepository affiliateProductRepository)
        {
            _apRepository = affiliateProductRepository;
        }

        [HttpPost]
        public ActionResult Deny(int affiliateProductId)
        {
            _apRepository.DenyAffiliateProduct(affiliateProductId);
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