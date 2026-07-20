using Biblioteca_web.Entity;
using Biblioteca_web.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca_web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Categoria
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ECategoria>>> GetCategorias()
        {
            return await _context.Categorias.ToListAsync();
        }

        // GET: api/Categoria/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ECategoria>> GetCategoria(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);

            if (categoria == null)
            {
                return NotFound(new { mensaje = $"No se encontró la categoría con ID {id}" });
            }

            return Ok(categoria);
        }

        // POST: api/Categoria
        [HttpPost]
        public async Task<ActionResult<ECategoria>> CreateCategoria([FromBody] ECategoria categoria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCategoria), new { id = categoria.Id }, categoria);
        }

        // PUT: api/Categoria/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategoria(int id, [FromBody] ECategoria categoria)
        {
            if (id != categoria.Id)
            {
                return BadRequest(new { mensaje = "El ID de la URL no coincide con el objeto enviado." });
            }

            var categoriaExistente = await _context.Categorias.FindAsync(id);
            if (categoriaExistente == null)
            {
                return NotFound(new { mensaje = $"No existe una categoría con el ID {id}" });
            }

            categoriaExistente.Nombre = categoria.Nombre;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Categoria/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return NotFound(new { mensaje = $"No existe una categoría con el ID {id}" });
            }

            try
            {
                _context.Categorias.Remove(categoria);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                // Maneja la restricción ON DELETE RESTRICT establecida en PostgreSQL
                return BadRequest(new { mensaje = "No se puede eliminar la categoría porque tiene libros asociados." });
            }

            return NoContent();
        }
    }

}