using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Shopping_Tutorial.Models
{
    public class DatabaseContext: IdentityDbContext<AppUserModel>
    {
        public DatabaseContext(DbContextOptions options):base(options) { }
        public DbSet<BrandModel> Brands { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<ProductModel> Products { get; set; }
    }
}
