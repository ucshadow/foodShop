using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodStore.Entities
{
    public class Sticker
    {
        public int StickerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ExpirationDate { get; set; }
        public string ProductsAppliedTo { get; set; }
    }
}