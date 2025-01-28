
using PixelPlay.Models;

namespace PixelPlay.Data
{
    public sealed class MyDbContext : DbContext
    {
        public MyDbContext() : base()
        {
            
        }

        public MyDbContext(DbContextOptions<MyDbContext> options):base(options) 
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameDevices>().HasKey(e => new { e.GameId, e.DeviceId });

            modelBuilder.Entity<GameCategories>().HasKey(e => new { e.GameId, e.CategoryId });
        }

        public DbSet<Games> Games { get; set; }
        public DbSet<Devices> Devices { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<GameDevices> GameDevices { get; set; }
        public DbSet<GameCategories> GameCategories { get; set; }

    }
}
