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
        public async Task<IActionResult> AddCarrito(int IdUsuario, int IdProducto, int precio, int cantidad)
        {
            var carritoToAdd = await new FunLandContext().Carritos.Where(x => x.IdUsuario==IdUsuario && x.IdProducto==IdProducto).SingleOrDefaultAsync();

            if(carritoToAdd is not null)
                return Ok(carritoToAdd);
                bool existProduct = await new FunLandContext().Productos.AnyAsync(r => r.IdProducto == IdProducto);

            if (!existProduct)
            { return NotFound($"No existe el producto {IdProducto}"); }

            bool existUsuario = await new FunLandContext().Usuarios.AnyAsync(r => r.IdUsuario == IdUsuario);
            if (!existUsuario)
            { return NotFound($"No existe el usuario {IdUsuario}"); }

            carritoToAdd = new Carrito();
                carritoToAdd.Precio = precio;
                carritoToAdd.Cantidad = cantidad;               
            
            var newCarrito = await new FunLandContext().Carritos.AddAsync(carritoToAdd);

            return Ok(newCarrito);         
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var carrito = await new FunLandContext().Usuarios.FirstOrDefaultAsync(d => d.IdUsuario == id);

            if (carrito is null)
                return BadRequest("El id indicado no existe");

            return Ok(carrito);
        }


        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await new FunLandContext().Usuarios.ToListAsync());


        [HttpPut]
        public async Task<IActionResult> Update(int id, int cantidad, int precio)
        {
            using (var ctx = new FunLandContext())
            {
                var carritoToUpdate = await ctx.Carritos.FirstOrDefaultAsync(d => d.IdUsuario == id);

                if (carritoToUpdate is null)
                    return BadRequest($"La review {id} no existe");

                carritoToUpdate.Precio = precio;
                carritoToUpdate.Cantidad = cantidad;
                ctx.SaveChanges();

                return Ok(carritoToUpdate);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            using (var ctx = new FunLandContext())
            {
                var carritoToDelete = await ctx.Usuarios.FindAsync(id);

                if (carritoToDelete is null)
                    return BadRequest("El id indicado no existe");

                ctx.Remove(carritoToDelete);
                ctx.SaveChanges();
                return Ok();
            }
        }

    }
}
