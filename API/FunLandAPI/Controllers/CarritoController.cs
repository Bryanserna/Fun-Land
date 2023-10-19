using FunLandAPI.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FunLandAPI.Controllers
{
    [ApiController]
    [Route("[Controller]/[Action]")]
    public class CarritoController : ControllerBase
    {
        [HttpPost(Name = "Add")]

        public async Task<IActionResult> Add(string escripcion)
        {
            var carritoToAdd = await new FunLandContext().Carritos.Where(x => x.IdUsuario == id).SingleOrDefaultAsync();
        }

    }
}
