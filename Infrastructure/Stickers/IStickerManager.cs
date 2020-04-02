using FoodStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodStore.Infrastructure.Stickers
{
    public interface IStickerManager
    {
        bool IsStickerOnProduct(int productId);
        List<Sticker> GetStickersOnProduct(int productId);
    }
}
