using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca_web.Entity
{
    [Table("autor")]
    public class EAutor
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        [Column("nombre")]
        public string Nombre { get; set; } = string.Empty;

        public ICollection<ELibro> Libros { get; set; } = new List<ELibro>();
    }
}
