using Microsoft.EntityFrameworkCore;
using PtcApi.Model;

namespace PtcApi.Data
{
    public class PtcDbContext : DbContext
    {
        public PtcDbContext(DbContextOptions<PtcDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<AppUser> Users{ get; set; }
        public DbSet<AppUserClaim> Claims { get; set; }
    }
}
