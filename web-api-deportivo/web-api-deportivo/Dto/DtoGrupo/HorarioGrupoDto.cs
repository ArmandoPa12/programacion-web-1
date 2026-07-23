namespace web_api_deportivo.Dto.DtoGrupo
{
    public class HorarioGrupoDto
    {
        public int Id { get; set; }
        public int DiaSemana { get; set; }
        public TimeOnly HoraInicio { get; set; }
        public TimeOnly HoraFin { get; set; }
    }
}
