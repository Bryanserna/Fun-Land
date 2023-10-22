using FunLandAPI.Database;
using FunLandAPI.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FunLandAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class GeneroController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Add(string descripcion)
        {
            using (var ctx = new FunLandContext())
            {
                var entity = await ctx.Generos.Where(x => x.Descripcion == descripcion).SingleOrDefaultAsync();

                if (entity is not null)
                    return BadRequest("El registro ya existe en la base de datos");

                var entityEntry = await ctx.Generos.AddAsync(new Genero { Descripcion = descripcion, Activo = true });
                ctx.SaveChanges();

                var newGenero = new GeneroDTO
                {
                    Id = entityEntry.Entity.IdGenero,
                    Descripcion = entityEntry.Entity.Descripcion ?? "",
                    Activo = entityEntry.Entity.Activo ?? false
                };

                return Ok(newGenero);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var entity = await new FunLandContext().Generos.Where(x => x.IdGenero == id).SingleOrDefaultAsync();

            if (entity is null)
                return NotFound($"El id {id} no existe");

            var genero = new GeneroDTO
            {
                Id = entity.IdGenero,
                Descripcion = entity.Descripcion ?? "",
                Activo = entity.Activo ?? false
            };

            return Ok(genero);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var entities = await new FunLandContext().Generos.ToListAsync();


            List<GeneroDTO> generos = entities.Select(x => new GeneroDTO
            {
                Id = x.IdGenero,
                Descripcion = x.Descripcion ?? "",
                Activo = x.Activo ?? false
            }).ToList();

            return Ok(generos);
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id, string descripcion)
        {
            using (var ctx = new FunLandContext())
            {
                var entityToUpdate = await ctx.Generos.Where(x => x.IdGenero == id).SingleOrDefaultAsync();

                if (entityToUpdate is null)
                    return BadRequest($"El id {id} no existe");

                entityToUpdate.Descripcion = descripcion;
                ctx.SaveChanges();

                var generoUpdated = new GeneroDTO
                {
                    Id = entityToUpdate.IdGenero,
                    Descripcion = entityToUpdate.Descripcion ?? "",
                    Activo = entityToUpdate.Activo ?? false
                };

                return Ok(generoUpdated);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            using (var ctx = new FunLandContext())
            {
                var entityToDelete = await ctx.Generos.Where(x => x.IdGenero == id).SingleOrDefaultAsync();

                if (entityToDelete is null)
                    return BadRequest($"El id {id} no existe");

                entityToDelete.Activo = false;
                ctx.SaveChanges();

                var generoDeleted = new GeneroDTO
                {
                    Id = entityToDelete.IdGenero,
                    Descripcion = entityToDelete.Descripcion ?? "",
                    Activo = entityToDelete.Activo ?? false
                };

                return Ok(generoDeleted);
            }
        }
    }
}
