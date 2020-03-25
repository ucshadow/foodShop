using FoodStore.Abstract;
using FoodStore.Entities;
using System.Web.Mvc;
using FoodStore.HtmlHelpers;
using FoodStore.Models;
using System.Diagnostics;
using System.Linq;

namespace FoodStore.Controllers
{
    public class PublicProfileController : Controller
    {
        private readonly IPublicProfilesRepository _ppRepository;
        private readonly IPurchaseHistoryRepository _hRepository;

        public PublicProfileController(IPublicProfilesRepository repository, IPurchaseHistoryRepository historyRepository)
        {
            _ppRepository = repository;
            _hRepository = historyRepository;
        }

        [Authorize]
        [HttpPost]
        public ViewResult SaveProfile(PublicProfile profile)
        {
            _ppRepository.SaveProfile(profile, User.Identity.GetUserId());

            return View("~/Views/PublicProfile/Index.cshtml", new PublicProfileModel { Profile = profile });
        }

        [Authorize]
        [HttpPost]
        public ViewResult EditProfile(PublicProfile profile)
        {
            _ppRepository.EditProfile(profile, User.Identity.GetUserId());

            return View("~/Views/PublicProfile/Index.cshtml", new PublicProfileModel { Profile = profile });
        }

        [Authorize]
        public ActionResult Index()
        {
            var profileModel = new PublicProfileModel
            {
                Profile = _ppRepository.GetPublicProfile(User.Identity.GetUserId())
            };
            return View(profileModel);
        }

        public ViewResult GetPublicProfile(string name)
        {
            var profile = _ppRepository.GetPublicProfileByName(name);
            if(profile == null)
            {
                ViewBag.Message = name;
                return View("~/Views/Shared/Error404.cshtml");
            }
            if(profile.ShowPurchaseHistory)
            {
                ViewBag.PH = _hRepository.PurchaseHistory.Where(e => e.UserId == profile.UserId)?.ToList();
            }
            return View("~/Views/PublicProfile/PublicProfile.cshtml",
                new PublicProfileModel { Profile = profile });
        }
    }
}