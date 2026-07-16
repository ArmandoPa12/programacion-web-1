namespace web_api_deportivo.Dto
{
    public class GuardarRolDto
    {
        public string NombreRol { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public List<int> PermisosId { get; set; } = new List<int>();
    }
}
