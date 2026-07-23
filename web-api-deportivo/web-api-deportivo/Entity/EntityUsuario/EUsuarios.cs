using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using web_api_deportivo.Entity.EntityInscripcion;
using web_api_deportivo.Entity.EntityRoles;

namespace web_api_deportivo.Entity.EntityUsuario
{
    [Table("usuarios")]
    public class EUsuarios
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        
        [Required]
        [Column("nombre_completo")]
        public string Nombre_completo { get; set; }
        
        [Required]
        [Column("carnet_usuario")]
        public string Carnet_usuario { get; set; }

        [Required]
        [EmailAddress]
        [Column("email")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Column("password")]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Column("activo")]
        public bool Activo { get; set; } = true;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [Column("rol_id")]
        public int RolId { get; set; }
        [ForeignKey("RolId")]
        public ERoles Rol { get; set; }

        public ICollection<EInscripcion> InscripcionesRegistradas { get; set; } = new List<EInscripcion>();

        public ICollection<EAsistencia> AsistenciasRegistradas { get; set; } = new List<EAsistencia>();
    }
}
