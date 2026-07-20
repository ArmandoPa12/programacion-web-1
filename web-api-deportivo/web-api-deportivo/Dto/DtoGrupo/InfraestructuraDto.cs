namespace web_api_deportivo.Dto.DtoGrupo
{
    public class InfraestructuraDto
    {
        public int Id { get; set; }

        public string NombreEspacio { get; set; } = string.Empty;

        public string UbicacionDetalle { get; set; } = string.Empty;

        public bool Activo { get; set; } = true;
    }
}
