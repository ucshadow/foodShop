using FoodStore.HtmlHelpers;
using FoodStore.Infrastructure.Discounts;
using FoodStore.Infrastructure.LocalAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;

namespace FoodStore
{
    public static class Hooks
    {
        public static void AddCustomHooks()
        {
            new RealTimeSellData();
            GlobalProductCache.SetUp();
            DiscountProvider.DiscountsManager = new DiscountsOfTheDayAutoHandler();
        }
    }
}