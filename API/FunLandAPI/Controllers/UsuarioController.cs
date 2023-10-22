using FunLandAPI.Database;
using FunLandAPI.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FunLandAPI.Controllers
{

    [ApiController]
    [Route("[controller]/[action]")]
    public class UsuarioController : ControllerBase
    {
        [HttpPost]

        public async Task<IActionResult> Add(int IdUsuario, string Nombre, string Correo, string Password)
        {
            using (var ctx = new FunLandContext())
            {
                var entity = await ctx.Usuarios.FirstOrDefaultAsync(x => x.NombreUsuario == Nombre);

                if (entity is not null)
                    return BadRequest("El registro ya existe en la base de datos");

                var entityEntry = await ctx.Usuarios.AddAsync(new Usuario { NombreUsuario = Nombre,  Activo = true, Correo=Correo, Pass=Password });
                ctx.SaveChanges();

                var newUsuario = new UsuarioDTO
                {
                    Id = entityEntry.Entity.IdUsuario,
                    Nombre = entityEntry.Entity.NombreUsuario ?? "",
                    Correo=entityEntry.Entity.Correo,
                    Password=entityEntry.Entity.Pass,
                    Activo = entityEntry.Entity.Activo ?? false
                };

                return Ok(newUsuario);
            }

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
        public async Task<IActionResult> Update(int id, string nombre, string correo, string password)
        {
            using (var ctx = new FunLandContext())
            {
                var usuarioToUpdate = await ctx.Usuarios.FirstOrDefaultAsync(d => d.IdUsuario == id);

                if (usuarioToUpdate is null)
                    return BadRequest($"La review {id} no existe");

                usuarioToUpdate.NombreUsuario = nombre;
                usuarioToUpdate.Correo = correo;
                usuarioToUpdate.Pass = password;
                ctx.SaveChanges();

                return Ok(usuarioToUpdate);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            using (var ctx = new FunLandContext())
            {
                var usuarioToDelete = await ctx.Usuarios.FindAsync(id);

                if (usuarioToDelete is null)
                    return BadRequest("El id indicado no existe");

                ctx.Remove(usuarioToDelete);
                ctx.SaveChanges();
                return Ok();
            }
        }
    }
}
