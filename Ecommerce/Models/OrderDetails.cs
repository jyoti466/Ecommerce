using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models
{
    public class OrderDetails
    {

        public int id { get; set; }

        [Display(Name ="Order")]
        public int Orderid { get; set; }

        [Display(Name ="Product")]
        public int Productid { get; set; }

        [ForeignKey("Orderid")]
        public Order Order { get; set; }

        [ForeignKey("Productid")]
        public Products products { get; set; }

    }
}
