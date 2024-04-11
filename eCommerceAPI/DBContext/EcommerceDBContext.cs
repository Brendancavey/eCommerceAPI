using eCommerceAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eCommerceAPI.DBContext
{
    public class EcommerceDBContext: IdentityDbContext<ApplicationUser>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        public EcommerceDBContext(DbContextOptions<EcommerceDBContext> options) : base(options) { }
    }
}
