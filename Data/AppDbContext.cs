using Microsoft.EntityFrameworkCore;

namespace CsvImportDemo.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Example DbSet for products
        public DbSet<Models.Product> Products { get; set; }
    }
}
