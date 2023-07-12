using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models
{
    public class Products
    {
        public int id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string Image { get; set; }

        [Display(Name = "Product Color")]
        public string ProductColor { get; set; }

        [Required]
        [Display(Name = "Available")]
        public bool IsAvailable { get; set; }

        [Required]
        [Display(Name = "Product Type")]
        public int ProductTypeid { get; set; }


        [ForeignKey("ProductTypeid")]
        public ProductType ProductType { get; set; }

        [Required]
        [Display(Name = "Special Tag")]
        public int SpecialTagid { get; set; }
        [ForeignKey("SpecialTagid")]
        public SpecialTag SpecialTag { get; set; }
    }
}
