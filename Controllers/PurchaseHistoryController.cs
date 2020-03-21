using FoodStore.Abstract;
using FoodStore.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodStore.Controllers
{
    public class PurchaseHistoryController : Controller
    {

        private readonly IPurchaseHistoryRepository _pRepository;
        private readonly IProductRepository _repository;

        public PurchaseHistoryController(IPurchaseHistoryRepository repository, IProductRepository r)
        {
            _pRepository = repository;
            _repository = r;
        }

        // GET: PurchaseHistory
        [Authorize]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var history = _pRepository.PurchaseHistory.Where(e => e.UserId == userId);

            return View(new PurchaseHistory(history));
        }

        [Authorize]
        [HttpPost]
        public ActionResult Vote(CastVote castVote)
        {
            Debug.WriteLine(castVote.Rating);
            int rating = castVote.Rating;
            int purchaseId = castVote.PurchaseId;
            if (purchaseId <= 0) return Json(new { Result = "Please provide a valid purchase id" });
            var votedOn = _pRepository.PurchaseHistory.FirstOrDefault(e => e.PurchaseID == purchaseId);
            if (votedOn == null) return Json(new { Result = "No purchase with that id" });
            if (votedOn.Rating > 0) return Json(new { Result = "You already voted on that item" });
            if (rating < 1 || rating > 5) return Json(new { Result = "Rating must be between 1 and 5" });
            votedOn.Rating = rating;
            _pRepository.SavePurchase(votedOn);
            //return Json(new { Result = rating, OK = true });
            return CastVote(rating, votedOn.ProductId);

        }

        private ActionResult CastVote(int rating, int productId)
        {
            var voted = _repository.Products.FirstOrDefault(e => e.ProductID == productId);
            if (voted == null) return Json(new { Result = "That product no longer exists" });

            var bigRating = voted.Rating * voted.NumberOfVotes;
            bigRating += rating;
            voted.NumberOfVotes += 1;
            voted.Rating = bigRating / voted.NumberOfVotes;
            _repository.SaveProduct(voted);
            return Json(new { Result = rating, OK = true });
        }
    }

    public class CastVote
    {
        public int Rating { get; set; }
        public int PurchaseId { get; set; }
    }
}