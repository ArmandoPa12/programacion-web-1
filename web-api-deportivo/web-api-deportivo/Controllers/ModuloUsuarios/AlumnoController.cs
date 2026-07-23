using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using web_api_deportivo.Conection;
using web_api_deportivo.Dto.DtoUsuario;
using web_api_deportivo.IRepository;
using web_api_deportivo.Validator;

namespace web_api_deportivo.Controllers.ModuloUsuarios
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AlumnoController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IAlumnoRepository _alumnoRepository;
        private readonly AlumnoValidator _av;

        public AlumnoController(
            AppDbContext db,
            IAlumnoRepository alumnoRepository,
            AlumnoValidator av)
        {
            _db = db;
            _alumnoRepository = alumnoRepository;
            _av = av;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAll()
        {
            var alumnos = await _alumnoRepository.GetAllAsync();
            return Ok(alumnos);
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var alumno = await _alumnoRepository.GetByIdAsync(id);

            if (alumno == null)
                return NotFound(new
                {
                    mensaje = $"No se encontró el alumno con ID {id}."
                });

            return Ok(alumno);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] AlumnoDto dto)
        {
            if (dto == null)
                return BadRequest("Los datos del alumno son inválidos.");

            var validationResult = await _av.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => new
                {
                    campo = e.PropertyName,
                    error = e.ErrorMessage
                }));
            }

            var alumno = await _alumnoRepository.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = alumno.Id }, alumno);
        }

        [HttpPut]
        [Route("[action]/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AlumnoDto dto)
        {
            if (dto == null)
                return BadRequest("Datos inválidos.");

            dto.Id = id;

            var validationResult = await _av.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => new
                {
                    campo = e.PropertyName,
                    error = e.ErrorMessage
                }));
            }

            var actualizado = await _alumnoRepository.UpdateAsync(id, dto);

            if (!actualizado)
            {
                return NotFound(new
                {
                    mensaje = $"No se encontró el alumno con ID {id}."
                });
            }

            return Ok(new
            {
                mensaje = "Alumno actualizado correctamente."
            });
        }
        [HttpDelete]
        [Route("[action]/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _alumnoRepository.DeleteAsync(id);

            if (!eliminado)
            {
                return NotFound(new
                {
                    mensaje = $"No se encontró el alumno con ID {id}."
                });
            }

            return Ok(new
            {
                mensaje = "Alumno eliminado correctamente."
            });
        }
    }
}
