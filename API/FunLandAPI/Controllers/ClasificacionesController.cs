using FunLandAPI.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FunLandAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ClasificacionesController : ControllerBase
    {
     
        [HttpGet(Name = "Clasificaciones")]
        public async Task<IActionResult> Get()
        {
            var clasificaciones = await new FunLandContext().Clasificacions.ToListAsync();
            return Ok(clasificaciones);
        }
    }
}