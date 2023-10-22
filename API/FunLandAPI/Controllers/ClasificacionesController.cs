using FunLandAPI.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FunLandAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ClasificacionesController : ControllerBase
    {
        [HttpPost(Name ="Add")]
        public async Task<IActionResult> Add(string descripcion)
        {
            var clasificacionToAdd = await new FunLandContext().Clasificacions.Where(x=> x.Descripcion == descripcion).SingleOrDefaultAsync();

            if(clasificacionToAdd is null)
            {
                clasificacionToAdd = new Clasificacion();
                clasificacionToAdd.Descripcion = descripcion;
            }

            clasificacionToAdd.Activo = true;

            var newClasificacion = await new FunLandContext().Clasificacions.AddAsync(clasificacionToAdd);

            return Ok(newClasificacion.Entity);
        }

        [HttpGet(Name = "Get")]
        public async Task<IActionResult> Get(int id)
        {
            var clasificacion = await new FunLandContext().Clasificacions.Where(x => x.IdClasificacion == id).SingleOrDefaultAsync();

            if (clasificacion is null)
                return BadRequest("El id indicado no existe");

            return Ok(clasificacion);
        }

        [HttpGet(Name = "GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var clasificaciones = await new FunLandContext().Clasificacions.ToListAsync();
            return Ok(clasificaciones);
        }

        [HttpPut(Name = "Update")]
        public async Task<IActionResult> Update(int id,string descripcion)
        {
            using (var ctx = new FunLandContext())
            {
                var clasificacionToUpdate = await ctx.Clasificacions.Where(x => x.IdClasificacion == id).SingleOrDefaultAsync();

                if (clasificacionToUpdate is null)
                    return BadRequest("El id indicado no existe");

                clasificacionToUpdate.Descripcion = descripcion;
                ctx.SaveChanges();

                return Ok(clasificacionToUpdate);
            }
        }

        [HttpDelete(Name = "Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            using (var ctx = new FunLandContext())
            {
                var clasificacionToDelete = await ctx.Clasificacions.Where(x => x.IdClasificacion == id).SingleOrDefaultAsync();

                if (clasificacionToDelete is null)
                    return BadRequest("El id indicado no existe");

                clasificacionToDelete.Activo = false;
                ctx.SaveChanges();
                return Ok();
            }
        }
    }
}