using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using FoodStore.HtmlHelpers;

namespace FoodStore.Entities
{
    public class PublicProfile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Avatar { get; set; }

        [Required(ErrorMessage = "Please enter a name")]
        [UniqueNameValidation(typeof(string))]
        public string Name { get; set; }
        public string City { get; set; }
        public bool ShowPurchaseHistory { get; set; }
    }
}