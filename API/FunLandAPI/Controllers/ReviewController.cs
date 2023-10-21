using FunLandAPI.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FunLandAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ReviewController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AddReview(int idProducto, int idUsuario, string review)
        {
            var reviewToAdd = await new FunLandContext().Reviews.FirstOrDefaultAsync(x => x.IdProducto == idProducto && x.IdUsuario == idUsuario);

            if (reviewToAdd is not null)
                return Ok(reviewToAdd);
            bool existProduct = await new FunLandContext().Productos.AnyAsync(r => r.IdProducto == idProducto);
            if (!existProduct)
            { return NotFound($"No existe el producto {idProducto}"); }

            bool existUsuario = await new FunLandContext().Usuarios.AnyAsync(r => r.IdUsuario == idUsuario);
            if (!existUsuario)
            { return NotFound($"No existe el usuario {idUsuario}"); }

            var reviewAdded = await new FunLandContext().Reviews.AddAsync(new Review
            {
                IdProducto = idProducto,
                IdUsuario = idUsuario,
                Review1 = review,
                Fecha = DateTime.Now,
            });

            return Ok(reviewAdded);
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var review = await new FunLandContext().Reviews.FirstOrDefaultAsync(d => d.IdReview == id);

            if (review is null)
                return BadRequest("El id indicado no existe");

            return Ok(review);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await new FunLandContext().Reviews.ToListAsync());

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
