using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace web_api_deportivo.Entity.EntityGrupo
{
    [Table("infraestructuras")]
    public class EInfraestructura
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("nombre_espacio")]
        public string NombreEspacio { get; set; }

        [Column("ubicacion_detalle")]
        public string? UbicacionDetalle { get; set; }

        [Required]
        [Column("activo")]
        public bool Activo { get; set; } = true;
    }
}
