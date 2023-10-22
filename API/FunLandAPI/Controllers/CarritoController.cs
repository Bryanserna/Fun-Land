using FunLandAPI.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FunLandAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CarritoController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AddCarrito(int usuario, int producto, int precio, int cantidad)
        {
            var carritoToAdd = await new FunLandContext().Carritos.Where(x => x.IdUsuario==usuario && x.IdProducto==producto).SingleOrDefaultAsync();

            if(carritoToAdd is not null)
            
                return Ok(carritoToAdd);
                bool existProduct = await new FunLandContext().Productos.AnyAsync(r => r.IdProducto == idProducto);

                carritoToAdd = new Carrito();
                carritoToAdd.Precio = precio;
                carritoToAdd.Cantidad = cantidad;               
            
            var newCarrito = await new FunLandContext().Carritos.AddAsync(carritoToAdd);

            return Ok(newCarrito);
                    
        }

    }
}
