using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using web_api_deportivo.Entity.EntityUsuario;

namespace web_api_deportivo.Entity.EntityInscripcion
{
    [Table("asistencias")]
    public class EAsistencia
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("inscripcion_id")]
        public int InscripcionId { get; set; }

        [Required]
        [Column("fecha")]
        public DateTime Fecha { get; set; }

        [Required]
        [Column("estado_asistencia")]
        public string EstadoAsistencia { get; set; } = string.Empty;

        [Required]
        [Column("usuario_id")]
        public int UsuarioId { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        public EInscripcion Inscripcion { get; set; } = null!;

        public EUsuarios Usuario { get; set; } = null!;
    }
}
