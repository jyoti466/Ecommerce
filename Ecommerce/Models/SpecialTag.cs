using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class SpecialTag
    {
        public int id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
