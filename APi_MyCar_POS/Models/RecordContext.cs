using Microsoft.EntityFrameworkCore;

namespace APi_MyCar_POS.Models
{
    public class RecordContext : DbContext
    {
        public DbSet<Record> Records { get; set; }

        public RecordContext(DbContextOptions<RecordContext> options) 
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
