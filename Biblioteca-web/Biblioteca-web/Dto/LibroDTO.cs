namespace Biblioteca_web.Dto
{
    public class LibroResumenDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
    }
    public class CategoriaReadDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public List<LibroResumenDto> Libros { get; set; } = new();
    }
    public class AutorReadDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public List<LibroResumenDto> Libros { get; set; } = new();
    }
}
