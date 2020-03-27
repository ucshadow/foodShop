using FoodStore.Entities;
using FoodStore.Infrastructure.Cache;
using System.Collections.Generic;

namespace FoodStore.Models
{
    public class ProductModel : ICacheable
    {
        public Product Product { get; set; }
        public List<Product> Related { get; set; }
        public bool IsCommentAllowedForCurrentUser { get; set; }
        public List<Comment> OldUserComments { get; set; }
        public List<CommentUserModel> Comments { get; set; } = new List<CommentUserModel>();

        public ICacheable Cache()
        {
            // should I cache a reference or a copy?? debatable
            return GlobalCache.GetCache().CacheItem<ProductModel>(this);
        }

        public object GetUniqueIdentifier<T>()
        {
            if(typeof(T).Name.ToLower().Contains("string")) return Product.Name as string;
            return Product.ProductID;
        }
    }
}