using FoodStore.Abstract;
using FoodStore.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodStore.Controllers
{
    public class PurchaseHistoryController : Controller
    {

        private readonly IPurchaseHistoryRepository _pRepository;

        public PurchaseHistoryController(IPurchaseHistoryRepository repository)
        {
            _pRepository = repository;
        }

        // GET: PurchaseHistory
        [Authorize]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var history = _pRepository.PurchaseHistory.Where(e => e.UserId == userId);

            return View(new PurchaseHistory(history));
        }
    }
}