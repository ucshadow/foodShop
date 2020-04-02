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
    public class AdminStickerController : Controller
    {
        private readonly IStickerRepository _sRepository;
        private readonly IProductRepository _pRepository;

        public AdminStickerController(IStickerRepository stickerRepository, IProductRepository productRepository)
        {
            _sRepository = stickerRepository;
            _pRepository = productRepository;
        }

        [HttpPost]
        public ActionResult AddSticker(Sticker sticker)
        {
            _sRepository.SaveSticker(sticker);
            return Redirect(Request.UrlReferrer.ToString());
        }

        [HttpPost]
        public ActionResult DeleteSticker(int? stickerId)
        {
            if(stickerId.HasValue) _sRepository.DeleteSticker(stickerId.Value);
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}