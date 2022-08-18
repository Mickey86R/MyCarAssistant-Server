using Microsoft.EntityFrameworkCore;

namespace APi_MyCar_POS.Models
{
    public class AutoContext : DbContext
    {
        public DbSet<Auto> Auto { get; set; }
        public AutoContext(DbContextOptions<AutoContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
