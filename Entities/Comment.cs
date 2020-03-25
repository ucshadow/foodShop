using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FoodStore.Entities
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int ProductID { get; set; } // Purchase also has a refrence, but this allows for anon comments
        public string AspNetUserId { get; set; } // match the table / column name, should auto foreign key?
        public int PurchaseID { get; set; }
        public string Content { get; set; }
        public string CommentType { get; set; } // could be comment, question or answer
        public string AddedOn { get; set; } // dont want to deal with dates
        public int Likes { get; set; }
    }
}