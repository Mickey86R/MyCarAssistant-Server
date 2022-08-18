using Microsoft.EntityFrameworkCore;

namespace APi_MyCar_POS.Models
{
    public class ExpensContext: DbContext
    {
        public DbSet<Expens> Expenses { get; set; }
        public ExpensContext(DbContextOptions<ExpensContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}