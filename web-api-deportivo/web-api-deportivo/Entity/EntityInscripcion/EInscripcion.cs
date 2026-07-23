using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using web_api_deportivo.Entity.EntityGrupo;
using web_api_deportivo.Entity.EntityUsuario;

namespace web_api_deportivo.Entity.EntityInscripcion
{
    [Table("inscripciones")]
    public class EInscripcion
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("alumno_id")]
        public int AlumnoId { get; set; }

        [Required]
        [Column("grupo_taller_id")]
        public int GrupoTallerId { get; set; }

        [Required]
        [Column("usuario_id")]
        public int UsuarioId { get; set; }

        [Required]
        [Column("fecha_inscripcion")]
        public DateTime FechaInscripcion { get; set; }

        [Required]
        [Column("canal_registro")]
        public string CanalRegistro { get; set; } = string.Empty;

        [Required]
        [Column("activo")]
        public bool Activo { get; set; } = true;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }    

        public EAlumno Alumno { get; set; } = null!;

        public EGrupoTaller GrupoTaller { get; set; } = null!;

        public EUsuarios Usuario { get; set; } = null!;

        public ICollection<EAsistencia> Asistencias { get; set; }
            = new List<EAsistencia>();
    }
}
