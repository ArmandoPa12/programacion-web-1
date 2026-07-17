using System.ComponentModel.DataAnnotations.Schema;

namespace web_api_deportivo.Entity.EntityRoles
{
    [Table("rol_permiso")]
    public class ERolPermiso
    {
        [Column("rol_id")]
        public int RolId { get; set; }

        [Column("permiso_id")]
        public int PermisoId { get; set; }
    }
}
