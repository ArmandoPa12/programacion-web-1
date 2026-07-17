using System.ComponentModel.DataAnnotations.Schema;

namespace web_api_deportivo.Entity.EntityRoles
{
    [Table("permisos")]
    public class EPermisos
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("nombre_permiso")]
        public string NombrePermiso { get; set; } = string.Empty;

        [Column("descripcion")]
        public string Descripcion { get; set; } = string.Empty;
        public ICollection<ERoles> Roles { get; set; } = new List<ERoles>();
    }
}
