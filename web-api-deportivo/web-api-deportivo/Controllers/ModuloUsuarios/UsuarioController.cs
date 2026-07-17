
using Microsoft.AspNetCore.Mvc;
using web_api_deportivo.Conection;
using web_api_deportivo.Dto.DtoUsuario;
using Microsoft.EntityFrameworkCore;
using web_api_deportivo.Entity.EntityUsuario;
using web_api_deportivo.Validator;
using web_api_deportivo.Service;

namespace web_api_deportivo.Controllers.ModuloUsuarios
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly UsuarioValidator _uv;
        private readonly LoginValidator _lv;
        private readonly PasswordService _passwordService;
        private readonly JwtService _jwtService;

        public UsuarioController(
            AppDbContext db, 
            UsuarioValidator uv, 
            PasswordService passwordService,
            LoginValidator lv,
            JwtService jwtService
            )
        {
            _db = db;
            _uv = uv;
            _lv = lv;
            _passwordService = passwordService;
            _jwtService = jwtService;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAll()
        {
            var usuarios = await _db.Usuarios
                .Include(u => u.Rol)
                .Select(u => new UsuarioDto
                {
                    Id = u.Id,
                    Nombre_completo = u.Nombre_completo,
                    Carnet_usuario = u.Carnet_usuario,
                    Password = string.Empty,
                    Email = u.Email,
                    Activo = u.Activo,
                    RolId = u.RolId,
                    NombreRol = u.Rol.NombreRol,
                    DescripcionRol = u.Rol.Descripcion
                })
                .ToListAsync();

            return Ok(usuarios);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] UsuarioDto dto)
        {
            var validationResult = await _uv.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            var nuevoUsuario = new EUsuarios
            {
                Nombre_completo = dto.Nombre_completo,
                Carnet_usuario = dto.Carnet_usuario,
                Email = dto.Email,
                RolId = dto.RolId,
                Activo = true,
                Password = _passwordService.Hash(dto.Password),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _db.Usuarios.AddAsync(nuevoUsuario);
            await _db.SaveChangesAsync();

            return Ok(new { mensaje = "Usuario creado con éxito.", usuarioId = nuevoUsuario.Id });
        }

        [HttpPut]
        [Route("[action]/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UsuarioDto dto)
        {
            dto.Id = id;
            var validationResult = await _uv.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            var usuarioExistente = await _db.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
            if (usuarioExistente == null)
            {
                return NotFound(new { mensaje = "El usuario que intenta actualizar no existe en el sistema." });
            }

            usuarioExistente.Nombre_completo = dto.Nombre_completo;
            usuarioExistente.Carnet_usuario = dto.Carnet_usuario;
            usuarioExistente.Email = dto.Email;
            usuarioExistente.RolId = dto.RolId;
            usuarioExistente.Activo = dto.Activo;
            usuarioExistente.UpdatedAt = DateTime.UtcNow;
            if (!string.IsNullOrEmpty(dto.Password))
            {
                usuarioExistente.Password = _passwordService.Hash(dto.Password);
            }
            var entity = _db.Entry(usuarioExistente);

            Console.WriteLine(entity.Property(x => x.CreatedAt).Metadata.GetColumnType());
            Console.WriteLine(entity.Property(x => x.UpdatedAt).Metadata.GetColumnType());
            await _db.SaveChangesAsync();

            return Ok(new { mensaje = "Usuario actualizado correctamente." });
        }
        [HttpDelete]
        [Route("[action]/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 1)
            {
                return BadRequest(new
                {
                    mensaje = "El usuario administrador principal no puede ser eliminado."
                });
            }
            var usuario = await _db.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
            if (usuario == null)
            {
                return NotFound(new
                {
                    mensaje = "El usuario no existe."
                });
            }
            _db.Usuarios.Remove(usuario);
            await _db.SaveChangesAsync();
            return Ok(new
            {
                mensaje = "Usuario eliminado correctamente."
            });
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var validationResult = await _lv.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }
            var usuario = await _db.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (usuario == null)
            {
                return Unauthorized(new
                {
                    mensaje = "Correo o contraseña incorrectos."
                });
            }
            var passwordValido = _passwordService.Verify(dto.Password, usuario.Password);
            if (!passwordValido)
            {
                return Unauthorized(new
                {
                    mensaje = "Correo o contraseña incorrectos."
                });
            }
            if (!usuario.Activo)
            {
                return Unauthorized(new
                {
                    mensaje = "El usuario se encuentra inactivo."
                });
            }
            var token = _jwtService.GenerateToken(usuario);
            return Ok(new
            {
                mensaje = "Inicio de sesión exitoso.",
                token,
                usuario = new
                {
                    usuario.Id,
                    usuario.Nombre_completo,
                    usuario.Email,
                    Rol = usuario.Rol.NombreRol
                }
            });
        }
    }
}
