using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FoodStore.Entities
{
    public class Affiliate
    {
        [Key]
        public string AffiliateId { get; set; }
        public string UserId { get; set; }
        public string AffiliateName { get; set; }
        public ICollection<AffiliateSell> Sells { get; set; }
    }    
}