using FoodStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodStore.Models
{
    public class CommentUserModel
    {
        public Comment Comment { get; set; }
        public PublicProfile User { get; set; }
        public int? Rating { get; set; }
        public string PurchasedOn { get; set; }
    }
}