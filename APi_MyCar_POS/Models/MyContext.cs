using Microsoft.EntityFrameworkCore;

namespace APi_MyCar_POS.Models
{
    public class MyContext : DbContext
    {
        public DbSet<Auto> Auto { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Expens> Expens { get; set; }
        public DbSet<Record> Record { get; set; }
        public DbSet<T_O> T_O { get; set; }

        public MyContext(DbContextOptions<MyContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // использование Fluent API
            base.OnModelCreating(modelBuilder);

            /*
            modelBuilder.Entity<Auto>()
                .HasMany(a => a.Users)
                .WithMany(u => u.Autos)
                .UsingEntity(au => au.ToTable("AutoUser"));
            */

            modelBuilder
            .Entity<Auto>()
            .HasMany(a => a.Users)
            .WithMany(u => u.Autos)
            .UsingEntity<AutoUser>(
               j => j
                .HasOne(pt => pt.User)
                .WithMany(t => t.AutoUsers)
                .HasForeignKey(pt => pt.UserID),
            j => j
                .HasOne(pt => pt.Auto)
                .WithMany(p => p.AutoUsers)
                .HasForeignKey(pt => pt.AutoID),
            j =>
            {
                j.HasKey(t => new { t.AutoID, t.UserID});
                j.ToTable("AutoUser");
            });
        }

    }
}
