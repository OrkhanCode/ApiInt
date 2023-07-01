using API_Intro.Models;
using System.Diagnostics.Metrics;

namespace API_Intro.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> option) : base(option)
        {
        }
        public DbSet<Country> Countries { get; set; }
    }
}
