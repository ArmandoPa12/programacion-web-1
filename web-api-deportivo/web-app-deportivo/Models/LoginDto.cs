using System.ComponentModel.DataAnnotations;

namespace web_app_deportivo.Models
{
    public class LoginDto
    {
        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "Formato de correo inválido")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public string Password { get; set; } = string.Empty;
    }

    // Lo que te devuelve la API (La respuesta completa)
    public class LoginResponseDto
    {
        public string Mensaje { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public UsuarioInfoDto Usuario { get; set; } = new();
    }

    // El objeto interno con los datos del usuario
    public class UsuarioInfoDto
    {
        public int Id { get; set; }
        public string Nombre_Completo { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
    }
}
