using System.Collections.Generic;

namespace Ecommerce.Models
{
    public class SQLProductrepository : Iproductrepository
    {
        private readonly AppDbContext context;

        public SQLProductrepository(AppDbContext context)
        {
            this.context = context;
        }
        //public IEnumerable<Product> GetAllProduct()
        //{
        //    return context.Products;
        //}

        //public Product GetProduct(int id)
        //{
        //    return context.Products.Find(id);

        //}
    }
}
