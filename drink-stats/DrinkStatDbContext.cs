using drink_stats.Drinks;
using Microsoft.EntityFrameworkCore;

namespace drink_stats
{
    public class DrinkStatDbContext : DbContext
    {
        public DrinkStatDbContext(DbContextOptions<DrinkStatDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Drink>()
                .HasIndex(d => d.Name)
                .IsUnique();
        }

        public DbSet<Drink> Drinks { get; set; }
    }
}
