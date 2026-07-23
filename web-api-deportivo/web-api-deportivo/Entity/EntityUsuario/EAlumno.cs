using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using web_api_deportivo.Entity.EntityInscripcion;

namespace web_api_deportivo.Entity.EntityUsuario
{
    [Table("alumnos")]
    public class EAlumno
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]        
        [Column("carnet_alumno")]
        public string carnet_alumno{ get; set; }

        [Required]
        [Column("nombre_alumno")]
        public string Nombre_Alumnos { get; set; }
        
        [Required]
        [Column("apellidos_alumno")]
        public string Apellidos_alumno { get; set; }

        [Required]
        [Column("fecha_nacimiento")]
        public DateTime Fecha_nacimiento { get; set; }

        [Column("alergias")]
        public string? Alergias { get; set; } = string.Empty;

        [Column("condiciones_medicas")]
        public string? Condiciones_Medicas { get; set; } = string.Empty;

        [Required]
        [Column("activo")]
        public bool Activo { get; set; } = true;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }

        public ICollection<EContactoEmergencias> ContactosEmergencia { get; set; }
            = new List<EContactoEmergencias>();
        public ICollection<EInscripcion> Inscripciones { get; set; }
            = new List<EInscripcion>();
    }
}
