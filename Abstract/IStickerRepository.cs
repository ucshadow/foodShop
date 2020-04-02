using FoodStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodStore.Abstract
{
    public interface IStickerRepository
    {
        Sticker GetStickerById(int stickerId);
        Sticker GetStickerByName(string name);
        List<Sticker> GetAllStickers();
        Task<Sticker> SaveSticker(Sticker sticker);
        Task<Sticker> DeleteSticker(int stickerId);
        List<Sticker> GetStickersOnProduct(int productId);
        bool IsOnProduct(int productId);
        void AddProduct(int stickerId, int productId);
        void RemoveProduct(int stickerId, int productId);
        void ClearProducts(int stickerId);
    }
}
