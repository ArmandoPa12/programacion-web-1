using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using web_api_deportivo.Entity.EntityUsuario;
using web_api_deportivo.Entity.EntityInscripcion;

namespace web_api_deportivo.Entity.EntityGrupo
{
    [Table("grupos_talleres")]
    public class EGrupoTaller
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("taller_id")]
        public int TallerId { get; set; }

        [Required]
        [Column("infraestructura_id")]
        public int InfraestructuraId { get; set; }

        [Required]
        [Column("profesor_id")]
        public int ProfesorId { get; set; }

        [Column("nombre_grupo")]
        public string NombreGrupo { get; set; } = string.Empty;

        [Required]
        [Column("cupo_maximo")]
        public int CupoMaximo { get; set; }

        [Required]
        [Column("activo")]
        public bool Activo { get; set; } = true;
        
        public ETaller Taller { get; set; } = null!;

        public EInfraestructura Infraestructura { get; set; } = null!;

        public EUsuarios Profesor { get; set; } = null!;

        public ICollection<EHorarioGrupo> Horarios { get; set; } = new List<EHorarioGrupo>();
        public ICollection<EInscripcion> Inscripciones { get; set; } = new List<EInscripcion>();
    }
}
