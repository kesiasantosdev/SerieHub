using Microsoft.EntityFrameworkCore;

namespace SerieHubAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Models.Usuarios> Usuarios { get; set; }
        public DbSet<Models.Serie> Series { get; set; }
    }
}
