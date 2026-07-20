using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using web_api_deportivo.Dto.DtoGrupo;
using web_api_deportivo.IRepository;
using web_api_deportivo.Validator;

namespace web_api_deportivo.Controllers.ModuloGrupo
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfraestructuraController : ControllerBase
    {
        private readonly IInfraestructuraRepository _infraestructuraRepository;
        private readonly InfraestructuraValidator _validator;

        public InfraestructuraController(
            IInfraestructuraRepository infraestructuraRepository,
            InfraestructuraValidator validator)
        {
            _infraestructuraRepository = infraestructuraRepository;
            _validator = validator;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAll()
        {
            var lista = await _infraestructuraRepository.GetAllAsync();
            return Ok(lista);
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var infraestructura = await _infraestructuraRepository.GetByIdAsync(id);

            if (infraestructura == null)
            {
                return NotFound(new
                {
                    mensaje = $"No se encontró la infraestructura con ID {id}."
                });
            }

            return Ok(infraestructura);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] InfraestructuraDto dto)
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

            var infraestructura = await _infraestructuraRepository.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById),
                new { id = infraestructura.Id },
                infraestructura);
        }

        [HttpPut]
        [Route("[action]/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] InfraestructuraDto dto)
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

            var actualizado = await _infraestructuraRepository.UpdateAsync(id, dto);

            if (!actualizado)
            {
                return NotFound(new
                {
                    mensaje = $"No se encontró la infraestructura con ID {id}."
                });
            }

            return Ok(new
            {
                mensaje = "Infraestructura actualizada correctamente."
            });
        }

        [HttpDelete]
        [Route("[action]/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _infraestructuraRepository.DeleteAsync(id);

            if (!eliminado)
            {
                return NotFound(new
                {
                    mensaje = $"No se encontró la infraestructura con ID {id}."
                });
            }

            return Ok(new
            {
                mensaje = "Infraestructura eliminada correctamente."
            });
        }
    }
}
