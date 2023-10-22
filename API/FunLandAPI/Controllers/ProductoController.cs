using FunLandAPI.Database;
using FunLandAPI.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FunLandAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ProductoController : ControllerBase
    {
        private FunLandContext context;

        public ProductoController()
        {
            context = new FunLandContext();
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductoRequestDto p)
        {
            var productoExistente = await context.Productos.FirstOrDefaultAsync(r => r.NombreProducto == p.NombreProducto);
            if (productoExistente is not null)
            {
                productoExistente.Activo = true;
                return Ok(productoExistente);
            }

            bool existClasificacion = await context.Clasificacions.AnyAsync(r => r.IdClasificacion == p.IdClasificacion);
            if (!existClasificacion)
            { return NotFound($"No existe la clasificacion {p.IdClasificacion}"); }

            bool existGenero = await context.Generos.AnyAsync(r => r.IdGenero == p.IdGenero);
            if (!existGenero)
            { return NotFound($"No existe el genero {p.IdGenero}"); }

            if (p.Precio <= 0)
            { return BadRequest("El precio no es correcto"); }

            var productoEntity = new Producto
            {
                IdGenero = p.IdGenero,
                IdClasificacion = p.IdClasificacion,
                FechaLanzamiento = p.FechaLanzamiento,
                NombreProducto = p.NombreProducto,
                Precio = p.Precio,
                Descripcion = p.Descripcion,
                Activo = true
            };

            await context.Productos.AddAsync(productoEntity);
            await context.SaveChangesAsync();

            return Ok(productoEntity);
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var producto = await context.Productos.AsNoTracking().FirstOrDefaultAsync(p => p.IdProducto == id);

            if (producto is null)
                return BadRequest("El id indicado no existe");

            return Ok(producto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var productos = await context.Productos.Where(p => p.Activo == true).AsNoTracking().ToListAsync();
            var productosResult = productos.Select(p => new ProductoDto
            {
                IdProducto = p.IdProducto,
                IdClasificacion = p?.IdClasificacion ?? 0,
                IdGenero = p?.IdGenero ?? 0,
                FechaLanzamiento = p.FechaLanzamiento ?? DateTime.Now,
                NombreProducto = p.NombreProducto ?? "",
                Precio = p?.Precio ?? 0,
                Descripcion = p.Descripcion ?? "",
                Activo = p.Activo ?? true,
            });

            return Ok(productosResult);
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductoDto p)
        {
            var producto = await context.Productos.FindAsync(p.IdProducto);
            if (producto is null)
            { return NotFound($"No existe el producto {p.IdProducto}"); }

            bool existClasificacion = await context.Clasificacions.AnyAsync(r => r.IdClasificacion == p.IdClasificacion);
            if (!existClasificacion)
            { return NotFound($"No existe la clasificacion {p.IdClasificacion}"); }

            bool existGenero = await context.Generos.AnyAsync(r => r.IdGenero == p.IdGenero);
            if (!existGenero)
            { return NotFound($"No existe el genero {p.IdGenero}"); }

            producto.IdGenero = p.IdGenero;
            producto.IdClasificacion = p.IdClasificacion;
            producto.FechaLanzamiento = p.FechaLanzamiento;
            producto.NombreProducto = p.NombreProducto;
            producto.Precio = p.Precio;
            producto.Descripcion = p.Descripcion;
            producto.Activo = true;

            context.Productos.Update(producto);
            await context.SaveChangesAsync();

            return Ok(producto);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            using (var ctx = new FunLandContext())
            {
                var productoToDelete = await ctx.Productos.FindAsync(id);

                if (productoToDelete is null)
                    return BadRequest("El id indicado no existe");

                ctx.Remove(productoToDelete);
                ctx.SaveChanges();
                return Ok();
            }
        }
    }
}
