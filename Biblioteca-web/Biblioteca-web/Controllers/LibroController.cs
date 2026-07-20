using Biblioteca_web.Dto;
using Biblioteca_web.Entity;
using Biblioteca_web.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca_web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibroController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LibroController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Libro
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LibroReadDto>>> GetLibros()
        {
            var libros = await _context.Libros
                .Include(l => l.Autor)
                .Include(l => l.Categoria)
                .Select(l => new LibroReadDto
                {
                    Id = l.Id,
                    Titulo = l.Titulo,
                    Descripcion = l.Descripcion,
                    IdAutor = l.IdAutor,
                    NombreAutor = l.Autor.Nombre,
                    IdCategoria = l.IdCategoria,
                    NombreCategoria = l.Categoria.Nombre
                })
                .ToListAsync();

            return Ok(libros);
        }

        // GET: api/Libro/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LibroReadDto>> GetLibro(int id)
        {
            var libro = await _context.Libros
                .Include(l => l.Autor)
                .Include(l => l.Categoria)
                .Where(l => l.Id == id)
                .Select(l => new LibroReadDto
                {
                    Id = l.Id,
                    Titulo = l.Titulo,
                    Descripcion = l.Descripcion,
                    IdAutor = l.IdAutor,
                    NombreAutor = l.Autor.Nombre,
                    IdCategoria = l.IdCategoria,
                    NombreCategoria = l.Categoria.Nombre
                })
                .FirstOrDefaultAsync();

            if (libro == null)
            {
                return NotFound(new { mensaje = $"No se encontró el libro con ID {id}" });
            }

            return Ok(libro);
        }

        // POST: api/Libro
        [HttpPost]
        public async Task<ActionResult<LibroReadDto>> CreateLibro([FromBody] LibroCreateUpdateDto dto)
        {
            // Validar que existan el Autor y la Categoría antes de insertar
            var autorExiste = await _context.Autores.AnyAsync(a => a.Id == dto.IdAutor);
            if (!autorExiste) return BadRequest(new { mensaje = $"El autor con ID {dto.IdAutor} no existe." });

            var categoriaExiste = await _context.Categorias.AnyAsync(c => c.Id == dto.IdCategoria);
            if (!categoriaExiste) return BadRequest(new { mensaje = $"La categoría con ID {dto.IdCategoria} no existe." });

            var libro = new ELibro
            {
                Titulo = dto.Titulo,
                Descripcion = dto.Descripcion,
                IdAutor = dto.IdAutor,
                IdCategoria = dto.IdCategoria
            };

            _context.Libros.Add(libro);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLibro), new { id = libro.Id }, dto);
        }

        // PUT: api/Libro/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLibro(int id, [FromBody] LibroCreateUpdateDto dto)
        {
            var libro = await _context.Libros.FindAsync(id);
            if (libro == null)
            {
                return NotFound(new { mensaje = $"No existe un libro con el ID {id}" });
            }

            var autorExiste = await _context.Autores.AnyAsync(a => a.Id == dto.IdAutor);
            if (!autorExiste) return BadRequest(new { mensaje = $"El autor con ID {dto.IdAutor} no existe." });

            var categoriaExiste = await _context.Categorias.AnyAsync(c => c.Id == dto.IdCategoria);
            if (!categoriaExiste) return BadRequest(new { mensaje = $"La categoría con ID {dto.IdCategoria} no existe." });

            libro.Titulo = dto.Titulo;
            libro.Descripcion = dto.Descripcion;
            libro.IdAutor = dto.IdAutor;
            libro.IdCategoria = dto.IdCategoria;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Libro/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLibro(int id)
        {
            var libro = await _context.Libros.FindAsync(id);
            if (libro == null)
            {
                return NotFound(new { mensaje = $"No existe un libro con el ID {id}" });
            }

            _context.Libros.Remove(libro);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
