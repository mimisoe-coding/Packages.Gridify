using Microsoft.EntityFrameworkCore;

namespace Packages.Gridify
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options): base(options) 
        {

        }
        public DbSet<BlogDataModel> Blogs { get; set; }
    }
}
