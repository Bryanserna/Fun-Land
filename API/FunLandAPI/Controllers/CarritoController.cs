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

        public async Task<IActionResult> Get(int usuario, int producto) Add(int precio, string cantidad)
        {
            var carritoToAdd = await new FunLandContext().Carritos.Where(x => x.IdUsuario).SingleOrDefaultAsync();

            if(carritoToAdd is null)
            {
                carritoToAdd = new Carrito();
               
            }
            
                    
        }

    }
}
