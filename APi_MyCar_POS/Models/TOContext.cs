using Microsoft.EntityFrameworkCore;

namespace APi_MyCar_POS.Models
{
    public class TOContext : DbContext
    {
        public DbSet<T_O> TOs{ get; set; }

        public TOContext(DbContextOptions<TOContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
