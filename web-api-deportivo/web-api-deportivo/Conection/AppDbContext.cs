using Microsoft.EntityFrameworkCore;
using web_api_deportivo.Entity.EntityGrupo;
using web_api_deportivo.Entity.EntityRoles;
using web_api_deportivo.Entity.EntityUsuario;

namespace web_api_deportivo.Conection
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base (options)
        {
        }
        public DbSet<ERoles> Roles { get; set; } = null;
        public DbSet<EPermisos> Permisos { get; set; } = null;
        // usuarios
        public DbSet<EUsuarios> Usuarios { get; set; } = null;
        public DbSet<EAlumno> Alumnos { get; set; } = null!;
        public DbSet<EContactoEmergencias> ContactosEmergencias { get; set; } = null!;
        // grupos
        public DbSet<EInfraestructura> Infraestructuras { get; set; } = null!;
        public DbSet<ETaller> Talleres { get; set; } = null!;



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ERoles>()
                .HasMany(r => r.Permisos)
                .WithMany(p => p.Roles)
                .UsingEntity<Dictionary<string, object>>(
                    "rol_permiso",
                    j => j.HasOne<EPermisos>().WithMany().HasForeignKey("permiso_id"),
                    j => j.HasOne<ERoles>().WithMany().HasForeignKey("rol_id")
                );


            modelBuilder.Entity<EUsuarios>()
                .HasOne(u => u.Rol) 
                .WithMany()
                .HasForeignKey(u => u.RolId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EContactoEmergencias>()
                .HasOne(c => c.Alumno)
                .WithMany(a => a.ContactosEmergencia)
                .HasForeignKey(c => c.Alumno_id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
