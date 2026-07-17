namespace web_api_deportivo.Dto.DtoUsuario
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string Nombre_completo { get; set; } = string.Empty;
        public string Carnet_usuario { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool Activo { get; set; }
        public int RolId { get; set; }
        public string? NombreRol { get; set; }
        public string? DescripcionRol { get; set; }
    }
}
