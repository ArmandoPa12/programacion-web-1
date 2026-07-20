using Biblioteca_web.Entity;
using Biblioteca_web.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca_web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AutorController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EAutor>>> GetAutores()
        {
            return await _context.Autores.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EAutor>> GetAutor(int id)
        {
            var autor = await _context.Autores.FindAsync(id);

            if (autor == null)
            {
                return NotFound(new { mensaje = $"No se encontró el autor con ID {id}" });
            }

            return Ok(autor);
        }

        [HttpGet("{id}/libros")]
        public async Task<ActionResult<EAutor>> GetAutorConLibros(int id)
        {
            var autor = await _context.Autores
                .Include(a => a.Libros)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (autor == null)
            {
                return NotFound(new { mensaje = $"No se encontró el autor con ID {id}" });
            }

            return Ok(autor);
        }

        [HttpPost]
        public async Task<ActionResult<EAutor>> CreateAutor([FromBody] EAutor autor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Autores.Add(autor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAutor), new { id = autor.Id }, autor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAutor(int id, [FromBody] EAutor autor)
        {
            if (id != autor.Id)
            {
                return BadRequest(new { mensaje = "El ID de la URL no coincide con el objeto enviado." });
            }

            var autorExistente = await _context.Autores.FindAsync(id);
            if (autorExistente == null)
            {
                return NotFound(new { mensaje = $"No existe un autor con el ID {id}" });
            }

            autorExistente.Nombre = autor.Nombre;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAutor(int id)
        {
            var autor = await _context.Autores.FindAsync(id);
            if (autor == null)
            {
                return NotFound(new { mensaje = $"No existe un autor con el ID {id}" });
            }

            _context.Autores.Remove(autor);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
