namespace web_api_deportivo.Dto.DtoRoles
{
    public class RolDto
    {
        // Datos principales del Rol
        public int Id { get; set; }
        public string NombreRol { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;

        // Lista de IDs: Se usa al CREAR o EDITAR (pueden venir vacíos)
        public List<int>? PermisosIds { get; set; }

        // Lista de Objetos: Se usa al LISTAR/OBTENER (el Frontend verá id, nombre y descripción)
        public List<PermisoDetalleDto>? Permisos { get; set; }
    }
}
