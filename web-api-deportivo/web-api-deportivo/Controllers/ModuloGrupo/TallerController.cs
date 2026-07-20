using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using web_api_deportivo.Dto.DtoGrupo;
using web_api_deportivo.IRepository;
using web_api_deportivo.Validator;

namespace web_api_deportivo.Controllers.ModuloGrupo
{
    [Route("api/[controller]")]
    [ApiController]
    public class TallerController : ControllerBase
    {
        private readonly ITallerRepository _tallerRepository;
        private readonly TallerValidator _validator;

        public TallerController(
            ITallerRepository tallerRepository,
            TallerValidator validator)
        {
            _tallerRepository = tallerRepository;
            _validator = validator;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAll()
        {
            var lista = await _tallerRepository.GetAllAsync();
            return Ok(lista);
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var taller = await _tallerRepository.GetByIdAsync(id);

            if (taller == null)
            {
                return NotFound(new
                {
                    mensaje = $"No se encontró el taller con ID {id}."
                });
            }

            return Ok(taller);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] TallerDto dto)
        {
            if (dto == null)
                return BadRequest("Datos inválidos.");

            var validation = await _validator.ValidateAsync(dto);

            if (!validation.IsValid)
            {
                return BadRequest(validation.Errors.Select(e => new
                {
                    campo = e.PropertyName,
                    error = e.ErrorMessage
                }));
            }

            var taller = await _tallerRepository.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById),
                new { id = taller.Id },
                taller);
        }

        [HttpPut]
        [Route("[action]/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TallerDto dto)
        {
            if (dto == null)
                return BadRequest("Datos inválidos.");

            dto.Id = id;

            var validation = await _validator.ValidateAsync(dto);

            if (!validation.IsValid)
            {
                return BadRequest(validation.Errors.Select(e => new
                {
                    campo = e.PropertyName,
                    error = e.ErrorMessage
                }));
            }

            var actualizado = await _tallerRepository.UpdateAsync(id, dto);

            if (!actualizado)
            {
                return NotFound(new
                {
                    mensaje = $"No se encontró el taller con ID {id}."
                });
            }

            return Ok(new
            {
                mensaje = "Taller actualizado correctamente."
            });
        }

        [HttpDelete]
        [Route("[action]/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _tallerRepository.DeleteAsync(id);

            if (!eliminado)
            {
                return NotFound(new
                {
                    mensaje = $"No se encontró el taller con ID {id}."
                });
            }

            return Ok(new
            {
                mensaje = "Taller eliminado correctamente."
            });
        }
    }
}
