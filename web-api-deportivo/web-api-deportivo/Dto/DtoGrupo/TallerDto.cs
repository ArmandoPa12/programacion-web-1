namespace web_api_deportivo.Dto.DtoGrupo
{
    public class TallerDto
    {
        public int Id { get; set; }

        public string NombreTaller { get; set; } = string.Empty;

        public string CategoriaEdad { get; set; } = string.Empty;

        public int EdadMinima { get; set; }

        public int EdadMaxima { get; set; }

        public bool Activo { get; set; } = true;
    }
}
