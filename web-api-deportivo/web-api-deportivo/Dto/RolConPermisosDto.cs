namespace web_api_deportivo.Dto
{
    public class RolConPermisosDto
    {
        public int Id { get; set; }
        public string NombreRol { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public List<PermisosDto> Permisos { get; set; } = new List<PermisosDto>();
    }
}
