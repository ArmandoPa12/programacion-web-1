using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using web_api_deportivo.Dto.DtoGrupo;
using web_api_deportivo.IRepository;
using web_api_deportivo.Repository;
using web_api_deportivo.Validator;

namespace web_api_deportivo.Controllers.ModuloGrupo
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrupoTallerController : ControllerBase
    {
        private readonly IGrupoTallerRepository _grupoRepository;
        private readonly GrupoTallerValidator _gv;
        public GrupoTallerController(IGrupoTallerRepository grupoRepository, GrupoTallerValidator gv)
        {
            _grupoRepository = grupoRepository;
            _gv = gv;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAll()
        {
            var grupos = await _grupoRepository.GetAllAsync();
            return Ok(grupos);
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var grupos = await _grupoRepository.GetByIdAsync(id);

            if (grupos == null)
            {
                return NotFound(new
                {
                    mensaje = $"No se encontró la infraestructura con ID {id}."
                });
            }

            return Ok(grupos);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] GrupoTallerDto dto)
        {
            if (dto == null)
                return BadRequest("Los datos del grupo son inválidos.");

            var validationResult = await _gv.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => new
                {
                    campo = e.PropertyName,
                    error = e.ErrorMessage
                }));
            }
            await _grupoRepository.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }

        [HttpDelete]
        [Route("[action]/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _grupoRepository.DeleteAsync(id);

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
