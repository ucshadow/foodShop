using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FoodStore.Entities
{
    public class Product
    {
        [HiddenInput(DisplayValue = false)]
        public int ProductID { get; set; }

        [Required(ErrorMessage = "Please enter a product name")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Please enter a description")]
        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Please specify a category")]
        public string Category { get; set; }

        [Required]
        [RegularExpression("g|ml|l|kg", ErrorMessage = "Unit should be g, ml, l or kg")]
        public string Unit { get; set; }

        [Required(ErrorMessage = "Please specify a quantity")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Please specify a size")]
        public decimal Size { get; set; }

        [Required(ErrorMessage = "Please specify a picture url")]
        public string Picture { get; set; }
    }
}
