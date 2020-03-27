using FoodStore.Abstract;
using FoodStore.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodStore.HtmlHelpers;
using FoodStore.Infrastructure.Cache;
using FoodStore.Models;

namespace FoodStore.Controllers
{
    public class CommentController : Controller
    {

        private readonly ICommentsRepository _cRepository;
        private readonly IPurchaseHistoryRepository _pRepository;

        public CommentController(ICommentsRepository repository,
            IPurchaseHistoryRepository purchaseHistoryRepository)
        {
            _cRepository = repository;
            _pRepository = purchaseHistoryRepository;
        }

        // GET: Comment
        public ActionResult Index()
        {
            return View();
        }

        // POST: Comment/Create
        [HttpPost]
        public ActionResult Create(string comment, int productID)
        {
            if(comment.Length > 500)
            {
                comment = comment.Substring(0, 500);
            }
            var c = new Comment
            {
                AddedOn = new DateTime().ToLocalTime().ToString(),
                AspNetUserId = User.Identity.GetUserId(),
                CommentType = "Comment",
                Content = comment,
                Likes = 0,
                ProductID = productID,
                PurchaseID = _pRepository.PurchaseHistory.FirstOrDefault(e => e.ProductId == productID).PurchaseID
            };

            // need to remove the cahed model from the cache so the comment is stored in the ProductModel cache
            GlobalCache.GetCache().ClearCachedItem<ProductModel>(productID);
            _cRepository.SaveComment(c);
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}
