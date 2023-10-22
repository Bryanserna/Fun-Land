using FunLandAPI.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FunLandAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ReviewController : ControllerBase
    {
        private FunLandContext context;

        public ReviewController()
        {
            context = new FunLandContext();
        }

        [HttpPost]
        public async Task<IActionResult> AddReview(int idProducto, int idUsuario, string review)
        {
            var reviewToAdd = await context.Reviews.FirstOrDefaultAsync(x => x.IdProducto == idProducto && x.IdUsuario == idUsuario);

            if (reviewToAdd is not null)
                return Ok(reviewToAdd);
            bool existProduct = await context.Productos.AnyAsync(r => r.IdProducto == idProducto);
            if (!existProduct)
            { return NotFound($"No existe el producto {idProducto}"); }

            bool existUsuario = await context.Usuarios.AnyAsync(r => r.IdUsuario == idUsuario);
            if (!existUsuario)
            { return NotFound($"No existe el usuario {idUsuario}"); }

            var reviewEntity = new Review
            {
                IdProducto = idProducto,
                IdUsuario = idUsuario,
                Review1 = review,
                Fecha = DateTime.Now,
            };

            await context.Reviews.AddAsync(reviewEntity);
            await context.SaveChangesAsync();

            return Ok(reviewEntity);
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var review = await new FunLandContext().Reviews.IgnoreAutoIncludes().AsNoTracking().FirstOrDefaultAsync(d => d.IdReview == id);

            if (review is null)
                return BadRequest("El id indicado no existe");

            return Ok(review);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await new FunLandContext().Reviews.AsNoTracking().ToListAsync());

        [HttpPut]
        public async Task<IActionResult> Update(int id, string review)
        {
            using (var ctx = new FunLandContext())
            {
                var reviewToUpdate = await ctx.Reviews.FirstOrDefaultAsync(d => d.IdReview == id);

                if (reviewToUpdate is null)
                    return BadRequest($"La review {id} no existe");

                reviewToUpdate.Review1 = review;
                reviewToUpdate.Fecha = DateTime.Now;
                ctx.SaveChanges();

                return Ok(reviewToUpdate);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            using (var ctx = new FunLandContext())
            {
                var reviewToDelete = await ctx.Reviews.FindAsync(id);

                if (reviewToDelete is null)
                    return BadRequest("El id indicado no existe");

                ctx.Remove(reviewToDelete);
                ctx.SaveChanges();
                return Ok();
            }
        }
    }
}
