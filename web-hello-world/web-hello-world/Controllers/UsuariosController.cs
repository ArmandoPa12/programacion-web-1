using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using web_hello_world.Conection;
using Microsoft.EntityFrameworkCore;
using web_hello_world.Repository;
using web_hello_world.Dto;
using FluentValidation;

namespace web_hello_world.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly IValidator<Users> _validator;
        public UsuariosController(IUserRepository repository, IValidator<Users>validator)
        {
            _repository = repository;
            _validator = validator;
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> get()
        {
            var usuarios = await _repository.GetAllAssync();
            return Ok(usuarios);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> create([FromBody] Users nuevo)
        {
            var validatorResult = await _validator.ValidateAsync(nuevo);
            if (!validatorResult.IsValid)
            {
                var errores = validatorResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { errores });
            }
            nuevo.CreatedAt = DateTime.UtcNow;
            var usuarioNuevo = await _repository.CreateAsync(nuevo);
            return Ok(usuarioNuevo);
        }
    }
}
