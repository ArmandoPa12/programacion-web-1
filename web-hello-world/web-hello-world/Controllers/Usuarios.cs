using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using web_hello_world.Conection;
using Microsoft.EntityFrameworkCore;

namespace web_hello_world.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Usuarios : ControllerBase
    {
        private readonly AppDbContext db;

        public Usuarios(AppDbContext db)
        {
            this.db = db;
        }


        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> get()
        {
            
            var usuarios = await db.Users.ToListAsync();

            return Ok(usuarios);    
        }
    }
}
