using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web_api_deportivo.Conection;
using web_api_deportivo.IRepository;

namespace web_api_deportivo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IRolesRepository _rolesRepository;

        // El sistema inyecta el DbContext automáticamente aquí
        public RolController(AppDbContext db, IRolesRepository rolesRepository)
        {
            _db = db;
            _rolesRepository = rolesRepository;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Get()
        {
            var roles = await _rolesRepository.GetAllAsync();
            return Ok(roles);
        }

    }
}
