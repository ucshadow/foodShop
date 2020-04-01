using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FoodStore.Entities
{
    public class AffiliateSell
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public string SellDate { get; set; }
        public int Quantity { get; set; }
        public decimal SellPrice { get; set; }
        public ShippingDetails ShippingDetails { get; set; }
    }
}