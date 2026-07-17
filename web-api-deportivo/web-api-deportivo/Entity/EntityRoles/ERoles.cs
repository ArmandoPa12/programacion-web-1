using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace web_api_deportivo.Entity.EntityRoles
{
    [Table("roles")]
    public class ERoles
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("nombre_rol")]
        public string NombreRol { get; set; } = string.Empty;

        [Column("descripcion")]
        public string Descripcion { get; set; } = string.Empty;
        public ICollection<EPermisos> Permisos { get; set; } = new List<EPermisos>();
    }
}
