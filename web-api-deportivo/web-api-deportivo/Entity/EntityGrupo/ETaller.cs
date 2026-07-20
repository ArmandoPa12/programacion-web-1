using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace web_api_deportivo.Entity.EntityGrupo
{
    [Table("talleres")]
    public class ETaller
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("nombre_taller")]
        public string NombreTaller { get; set; }

        [Column("categoria_edad")]
        public string? CategoriaEdad { get; set; }

        [Required]
        [Column("edad_minima")]
        public int EdadMinima { get; set; }

        [Required]
        [Column("edad_maxima")]
        public int EdadMaxima { get; set; }

        [Required]
        [Column("activo")]
        public bool Activo { get; set; } = true;
    }
}
