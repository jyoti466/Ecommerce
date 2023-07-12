using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class ProductType
    {
        public int id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
