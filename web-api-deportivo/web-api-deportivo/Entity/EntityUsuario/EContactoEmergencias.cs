using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace web_api_deportivo.Entity.EntityUsuario
{
    [Table("contactos_emergencia")]
    public class EContactoEmergencias
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("alumno_id")]
        public int Alumno_id { get; set; }

        [Required]
        [Column("nombre_completo")]
        public string Nombre_Completo { get; set; }

        [Required]
        [Column("telefono")]
        public string Telefono { get; set; }
        
        [Required]
        [Column("parentesco")]
        public string Parentesco { get; set; }

        [Column("prioridad")]
        public int Prioridad { get; set; } 

        [Required]
        [Column("activo")]
        public bool Activo { get; set; } = true;

        [ForeignKey(nameof(Alumno_id))]
        public EAlumno Alumno { get; set; }
    }

}
