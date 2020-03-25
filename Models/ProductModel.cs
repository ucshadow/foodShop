using FoodStore.Entities;
using System.Collections.Generic;

namespace FoodStore.Models
{
    public class ProductModel
    {
        public Product Product { get; set; }
        public List<Product> Related { get; set; }
        public bool IsCommentAllowedForCurrentUser { get; set; }
        public List<Comment> OldUserComments { get; set; }
        public List<CommentUserModel> Comments { get; set; } = new List<CommentUserModel>();
    }
}