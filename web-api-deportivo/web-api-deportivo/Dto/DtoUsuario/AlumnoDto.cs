namespace web_api_deportivo.Dto.DtoUsuario
{
    public class AlumnoDto
    {
        public int Id { get; set; }

        public string CarnetAlumno { get; set; } = string.Empty;

        public string NombreAlumno { get; set; } = string.Empty;

        public string ApellidosAlumno { get; set; } = string.Empty;

        public DateTime FechaNacimiento { get; set; }

        public string Alergias { get; set; } = string.Empty;

        public string CondicionesMedicas { get; set; } = string.Empty;

        public bool Activo { get; set; } = true;

        public List<ContactoEmergenciaDto> ContactosEmergencia { get; set; } = [];

        public int Edad
        {
            get
            {
                var hoy = DateTime.Today;

                int edad = hoy.Year - FechaNacimiento.Year;

                if (FechaNacimiento.Date > hoy.AddYears(-edad))
                    edad--;

                return edad;
            }
        }
    }
}
