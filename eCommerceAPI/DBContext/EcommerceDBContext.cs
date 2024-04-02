using eCommerceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace eCommerceAPI.DBContext
{
    public class EcommerceDBContext: DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        public EcommerceDBContext(DbContextOptions<EcommerceDBContext> options) : base(options) { }
    }
}
