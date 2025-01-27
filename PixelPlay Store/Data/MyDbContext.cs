
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
        }
    }
}
