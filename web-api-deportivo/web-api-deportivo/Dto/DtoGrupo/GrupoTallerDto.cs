namespace web_api_deportivo.Dto.DtoGrupo
{
    public class GrupoTallerDto
    {
        public int Id { get; set; }

        public int TallerId { get; set; }

        public int InfraestructuraId { get; set; }

        public int ProfesorId { get; set; }

        public string NombreGrupo { get; set; } = string.Empty;

        public int CupoMaximo { get; set; }

        public bool Activo { get; set; } = true;

        public string NombreTaller { get; set; } = string.Empty;

        public string NombreInfraestructura { get; set; } = string.Empty;

        public string NombreProfesor { get; set; } = string.Empty;

        public List<HorarioGrupoDto> Horarios { get; set; } = [];
    }
}
