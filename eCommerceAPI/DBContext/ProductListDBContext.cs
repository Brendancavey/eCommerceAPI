using eCommerceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace eCommerceAPI.DBContext
{
    public class ProductListDBContext: DbContext
    {
        public DbSet<Product> Products { get; set; }

        public ProductListDBContext(DbContextOptions<ProductListDBContext> options) : base(options) { }
    }
}
