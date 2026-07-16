using Microsoft.EntityFrameworkCore;
using web_api_deportivo.Entity;

namespace web_api_deportivo.Conection
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base (options)
        {
        }
        public DbSet<ERoles> Roles { get; set; } = null;
    }
}
