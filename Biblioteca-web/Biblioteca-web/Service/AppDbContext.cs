using Biblioteca_web.Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Biblioteca_web.Service
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<EAutor> Autores { get; set; } = null!;
        public DbSet<ECategoria> Categorias { get; set; } = null!;
        public DbSet<ELibro> Libros { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ELibro>()
                .HasOne(l => l.Autor)
                .WithMany(a => a.Libros)
                .HasForeignKey(l => l.IdAutor)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ELibro>()
                .HasOne(l => l.Categoria)
                .WithMany(c => c.Libros)
                .HasForeignKey(l => l.IdCategoria)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
