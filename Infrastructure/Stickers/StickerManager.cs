using FoodStore.Abstract;
using FoodStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodStore.Infrastructure.Stickers
{
    public class StickerManager : IStickerManager
    {

        private readonly IStickerRepository _sRepository;

        public StickerManager()
        {
            _sRepository = DependencyResolver.Current.GetService<IStickerRepository>();
        }

        public bool IsStickerOnProduct(int productId)
        {
            return _sRepository.IsOnProduct(productId);
        }

        public List<Sticker> GetStickersOnProduct(int productId)
        {
            return _sRepository.GetStickersOnProduct(productId);
        }
    }
}