using FunLandAPI.Database;
using FunLandAPI.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FunLandAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ClasificacionesController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Add(string descripcion)
        {
            using (var ctx = new FunLandContext())
            {
                var clasificacionToAdd = await ctx.Clasificacions.FirstOrDefaultAsync(x => x.Descripcion == descripcion);

                if (clasificacionToAdd is not null)
                    return BadRequest("El registro ya existe en la base de datos");

                var entityEntry = await ctx.Clasificacions.AddAsync(new Clasificacion { Descripcion = descripcion, Activo=true });
                ctx.SaveChanges();

                var newClasificacion = new ClasificacionDTO
                {
                    Id = entityEntry.Entity.IdClasificacion,
                    Descripcion = entityEntry.Entity.Descripcion ?? "",
                    Activo = entityEntry.Entity.Activo ?? false
                };

                return Ok(newClasificacion);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var entity = await new FunLandContext().Clasificacions.FirstOrDefaultAsync(x => x.IdClasificacion == id);

            if (entity is null)
                return NotFound($"El id {id} no existe");

            var clasificacion = new ClasificacionDTO
            {
                Id = entity.IdClasificacion,
                Descripcion = entity.Descripcion ?? "",
                Activo = entity.Activo ?? false
            };

            return Ok(clasificacion);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var entities = await new FunLandContext().Clasificacions.ToListAsync();


            List<ClasificacionDTO> clasificaciones = entities.Select(x => new ClasificacionDTO
            {
                Id = x.IdClasificacion,
                Descripcion = x.Descripcion ?? "",
                Activo = x.Activo ?? false
            }).ToList();

            return Ok(clasificaciones);
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id,string descripcion)
        {
            using (var ctx = new FunLandContext())
            {
                var entityToUpdate = await ctx.Clasificacions.FirstOrDefaultAsync(x => x.IdClasificacion == id);

                if (entityToUpdate is null)
                    return BadRequest($"El id {id} no existe");

                entityToUpdate.Descripcion = descripcion;
                ctx.SaveChanges();

                var clasificacionUpdated = new ClasificacionDTO
                {
                    Id = entityToUpdate.IdClasificacion,
                    Descripcion = entityToUpdate.Descripcion ?? "",
                    Activo = entityToUpdate.Activo ?? false
                };

                return Ok(clasificacionUpdated);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            using (var ctx = new FunLandContext())
            {
                var entityToDelete = await ctx.Clasificacions.FirstOrDefaultAsync(x => x.IdClasificacion == id);

                if (entityToDelete is null)
                    return BadRequest($"El id {id} no existe");

                entityToDelete.Activo = false;
                ctx.SaveChanges();

                var clasificacionDeleted = new ClasificacionDTO
                {
                    Id = entityToDelete.IdClasificacion,
                    Descripcion = entityToDelete.Descripcion ?? "",
                    Activo = entityToDelete.Activo ?? false
                };

                return Ok(clasificacionDeleted);
            }
        }
    }
}