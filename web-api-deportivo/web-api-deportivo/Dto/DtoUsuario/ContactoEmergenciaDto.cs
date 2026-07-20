namespace web_api_deportivo.Dto.DtoUsuario
{
    public class ContactoEmergenciaDto
    {
        public int Id { get; set; }
        public int AlumnoId { get; set; }

        public string NombreCompleto { get; set; } = string.Empty;

        public string Telefono { get; set; } = string.Empty;

        public string Parentesco { get; set; } = string.Empty;

        public int Prioridad { get; set; }

        public bool Activo { get; set; } = true;
    }
}
