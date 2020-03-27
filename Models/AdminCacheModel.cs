using FoodStore.Infrastructure.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodStore.Models
{
    // the admin can see a representation of the cache and act on it
    public class AdminCacheModel
    {
        public GlobalCache Cache = GlobalCache.GetCache();
    }
}