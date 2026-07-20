using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca_web.Entity
{
    [Table("libro")]
    public class ELibro
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        [Column("titulo")]
        public string Titulo { get; set; } = string.Empty;

        [Column("descripcion")]
        public string? Descripcion { get; set; }

        [Column("id_autor")]
        public int IdAutor { get; set; }

        [Column("id_categoria")]
        public int IdCategoria { get; set; }

        [ForeignKey(nameof(IdAutor))]
        public EAutor Autor { get; set; } = null!;

        [ForeignKey(nameof(IdCategoria))]
        public ECategoria Categoria { get; set; } = null!;
    }
}
