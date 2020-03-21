using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodStore.Entities
{
    public class Comment
    {
        public int ProductId { get; set; }
        public int ClientId { get; set; }
    }
}