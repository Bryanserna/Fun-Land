//using FunLandAPI.Database;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace FunLandAPI.Controllers
//{

//    [ApiController]
//    [Route("[controller]/[action]")]
//    public class UsuarioController : ControllerBase
//    {
//        [HttpPost]

//        public async Task<IActionResult> AddCarrito(int IdUsuario, string Nombre, string Correo, string Password)
//        {
//            var usuarioToAdd = await new FunLandContext().Usuarios.Where(x => x.IdUsuario == IdUsuario).SingleOrDefaultAsync();

//            if (usuarioToAdd is not null)
//            {
//                return Ok(usuarioToAdd);
//            }

//            usuarioToAdd.Activo = true;

//            var reviewAdded = await new FunLandContext().Usuarios.AddAsync(new Review
//            {
//                nombre = idProducto,
//                IdUsuario = idUsuario,
//                Review1 = review,
//                Fecha = DateTime.Now,

//                Nombre
//            });



//            var newCarrito = await new FunLandContext().Carritos.AddAsync(carritoToAdd);

//            return Ok(newCarrito);
//        }
//    }
//}
