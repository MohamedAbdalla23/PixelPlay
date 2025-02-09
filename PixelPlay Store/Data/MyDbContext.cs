
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
            modelBuilder.Entity<Categories>().HasData(
            [
                new Categories {Id=1, Name="Action" },
                new Categories {Id=2, Name="Adventure"},
                new Categories {Id=3, Name="Arcade"},
                new Categories {Id=4, Name="Horror"},
                new Categories {Id=5, Name="Fighting"},
                new Categories {Id=6, Name="Story"},
                new Categories {Id=7, Name="Shooting"},
                new Categories {Id=8, Name="Sport"},
                new Categories {Id=9, Name="Survival"},
                new Categories {Id=10, Name="Drama"}
            ]);

            modelBuilder.Entity<Devices>().HasData(
            [
                new Devices { Id=1, Name="Playstation", Icon="bi bi-playstation"},
                new Devices {Id=2, Name="Xbox", Icon="bi bi-xbox"},
                new Devices {Id=3, Name= "PC", Icon="bi bi-pc-display"},
                new Devices {Id=4, Name= "Nintendo Switch", Icon="bi bi-nintendo-switch"}
            ]);

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
