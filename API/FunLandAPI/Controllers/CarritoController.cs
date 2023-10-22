using FunLandAPI.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FunLandAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CarritoController : ControllerBase
    {
        //[HttpPost]
        //public async Task<IActionResult> AddCarrito(int usuario, int producto, int precio, int cantidad)
        //{
        //    var carritoToAdd = await new funlandcontext().carritos.where(x => x.idusuario == usuario && x.idproducto == producto).singleordefaultasync();

        //    if (carritotoadd is not null)

        //        return ok(carritotoadd);
        //    bool existproduct = await new funlandcontext().productos.anyasync(r => r.idproducto == idproducto);

        //    carritotoadd = new carrito();
        //    carritotoadd.precio = precio;
        //    carritotoadd.cantidad = cantidad;

        //    var newcarrito = await new funlandcontext().carritos.addasync(carritotoadd);

        //    return ok(newcarrito);

        //}

    }
}
