using FunLandAPI.Database;
using FunLandAPI.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FunLandAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class VentaController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AplicarVenta(int idUsuario)
        {
            using (var ctx = new FunLandContext())
            {
                var usuario = await ctx.Usuarios.FirstOrDefaultAsync(x => x.IdUsuario == idUsuario);

                if (usuario is null)
                    return NotFound("El usuario no existe");
                
                var carrito = await ctx.Carritos.Where(x => x.IdUsuario == usuario.IdUsuario).ToListAsync();

                if (carrito.Count() <= 0)
                    return BadRequest("El carrito no contiene registros");

                var productosCarrito  = carrito.Select(x => new Ventum
                {
                    IdUsuario = x.IdUsuario,
                    IdProducto = x.IdProducto,
                    PrecioU = x.Precio,
                    Cantidad = x.Cantidad,
                    Fecha = DateTime.Now,
                }).ToList();

                await ctx.Venta.AddRangeAsync(productosCarrito);
                ctx.Carritos.RemoveRange(carrito);

                ctx.SaveChanges();

                return Ok();
            }
        }
    }
}
