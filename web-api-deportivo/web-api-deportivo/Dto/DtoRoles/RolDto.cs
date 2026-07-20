namespace web_api_deportivo.Dto.DtoRoles
{
    public class RolDto
    {      
        public int Id { get; set; }
        public string NombreRol { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public List<int>? PermisosIds { get; set; }

        public List<PermisoDetalleDto>? Permisos { get; set; }
    }
}
