using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using web_api_deportivo.Conection;
using web_api_deportivo.IRepository;

namespace web_api_deportivo.Controllers.ModuloRoles
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PermisosController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IPermisosRepository _repo;

        public PermisosController(AppDbContext db, IPermisosRepository rolesRepository)
        {
            _db = db;
            _repo = rolesRepository;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAll()
        {
            var permisos = await _repo.GetAllAsync();
            return Ok(permisos);
        }

    }
}
