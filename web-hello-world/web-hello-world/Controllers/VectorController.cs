using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using web_hello_world.Dto;

namespace web_hello_world.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VectorController : ControllerBase
    {
        [HttpGet]
        [Route("[action]")]
        public IActionResult get()
        {
            return Ok("Hello worlas d");
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult calcularPromedio(List<Int32> vector)
        {
            if (vector == null || vector.Count == 0) return Ok(0);

            var sum = vector.Sum(x => x);
            var promedio = sum / vector.Count;
            return Ok(promedio);

        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult contarPares(List<Int64> vector)
        {
            Respuesta respuesta = new Respuesta();
            int contador = 0;
            try
            {
                foreach (var vectorItem in vector)
                {
                    if (vectorItem % 2 == 0)
                    {
                        contador++;
                    }
                        
                }
                respuesta.Success = false;
                
            }
            catch (Exception ex)
            {
                respuesta.Success = false;
                respuesta.ErrorMessage = ex.Message;
            }

            return Ok(respuesta);
        }

        [HttpGet]
        [Route("factorial/{n:int}")]
        public IActionResult factorial([FromRoute] int n)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                int fact = 0;
                if (n == 0 || n == 1)
                {
                    fact = 1;
                }
                for (int i = 1; i <= n; i++)
                {
                    fact *= i;
                }
                respuesta .Valor = fact;
                respuesta.Success = true;
            }
            catch (Exception)
            {
                respuesta.Valor = 0;
                respuesta.Success = false;
            }
            return Ok(respuesta);
        }
    }
}
