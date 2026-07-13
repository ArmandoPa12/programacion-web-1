using web_hello_world.Dto;
using Microsoft.EntityFrameworkCore;
namespace web_hello_world.Conection
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Users> Users { get; set; } = null!;
    }
}
