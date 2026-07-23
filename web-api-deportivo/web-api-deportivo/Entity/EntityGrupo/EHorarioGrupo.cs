using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace web_api_deportivo.Entity.EntityGrupo
{
    [Table("horarios_grupo")]
    public class EHorarioGrupo
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("grupo_id")]
        public int GrupoId { get; set; }

        [Required]
        [Column("dia_semana")]
        public int DiaSemana { get; set; }

        [Required]
        [Column("hora_inicio")]
        public TimeOnly HoraInicio { get; set; }

        [Required]
        [Column("hora_fin")]
        public TimeOnly HoraFin { get; set; }

        public EGrupoTaller Grupo { get; set; } = null!;
    }
}
