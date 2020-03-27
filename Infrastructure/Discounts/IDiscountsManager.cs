using FoodStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodStore.Infrastructure.Discounts
{
    public interface IDiscountsManager
    {
        List<Product> GetDiscounts();
        void RemoveProductIfExists(int productID);
        bool ProductExists(int productID);

        void SetMaxDiscountPercentage(int? percentage);
        void SetMaxProductsDiscounted(int? maxProducts);
        void SetCheapBias(int? cheapBias);
        void SetUpdateTimeInSeconds(int? seconds);
        void TriggerManualUpdate();

        int GetMaxDiscountPercentage();
        int GetMaxProductsDiscounted();
        int GetCheapBias();
        int GetUpdateTimeInSeconds();

        void AddItem(Product product, bool saveItem = true);
    }
}
