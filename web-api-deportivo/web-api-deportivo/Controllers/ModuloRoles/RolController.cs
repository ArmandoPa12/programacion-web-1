using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web_api_deportivo.Conection;
using web_api_deportivo.Dto.DtoRoles;
using web_api_deportivo.Entity.EntityRoles;
using web_api_deportivo.IRepository;
using web_api_deportivo.Validator;

namespace web_api_deportivo.Controllers.ModuloRoles
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IRolesRepository _rolesRepository;
        private readonly RolValidator _rv;
        public RolController(AppDbContext db, IRolesRepository rolesRepository, RolValidator rv)
        {
            _db = db;
            _rolesRepository = rolesRepository;
            _rv = rv;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _rolesRepository.GetAllAsync();
            return Ok(roles);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] RolDto dto)
        {
            if (dto == null) return BadRequest("Los datos del rol son inválidos.");
            var validationResult = await _rv.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => new { campo = e.PropertyName, error = e.ErrorMessage }));
            }
            var nuevoRol = new ERoles
            {
                NombreRol = dto.NombreRol,
                Descripcion = dto.Descripcion
            };
            if (dto.PermisosIds != null && dto.PermisosIds.Any())
            {
                var recuperado = await _db.Permisos.Where(p => dto.PermisosIds.Contains(p.Id)).ToListAsync();
                nuevoRol.Permisos = recuperado;
            }            

            _db.Roles.Add(nuevoRol);
            await _db.SaveChangesAsync();

            dto.Id = nuevoRol.Id;
            return CreatedAtAction(nameof(GetAll), new { id = nuevoRol.Id }, dto);
        }

        [HttpPut]
        [Route("[action]/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RolDto dto)
        {
            if (dto == null) return BadRequest("Datos inválidos.");
            var validationResult = await _rv.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => new { campo = e.PropertyName, error = e.ErrorMessage }));
            }

            var rolExistente = await _db.Roles
                .Include(r => r.Permisos)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (rolExistente == null) return NotFound($"No se encontró el rol con ID {id}");
            rolExistente.NombreRol = dto.NombreRol;
            rolExistente.Descripcion = dto.Descripcion;
            rolExistente.Permisos.Clear();
            if (dto.PermisosIds != null && dto.PermisosIds.Any())
            {
                var nuevosPermisos = await _db.Permisos
                    .Where(p => dto.PermisosIds.Contains(p.Id))
                    .ToListAsync();

                foreach (var permiso in nuevosPermisos)
                {
                    rolExistente.Permisos.Add(permiso);
                }
            }
            await _db.SaveChangesAsync();
            return Ok(new { mensaje = "Rol actualizado correctamente." });
        }

        [HttpDelete]
        [Route("[action]/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 1 || id == 2 || id == 3)
            {
                return BadRequest(new { mensaje = "No se pueden eliminar los roles base del sistema (Coordinador, Funcionario y Profesor)." });
            }
            var rol = await _db.Roles.FindAsync(id);
            if (rol == null) return NotFound($"No se encontró el rol con ID {id}");
            _db.Roles.Remove(rol);
            await _db.SaveChangesAsync();
            return Ok(new { mensaje = "Rol eliminado correctamente." });
        }

    }
}
