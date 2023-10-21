﻿using FunLandAPI.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FunLandAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CarritoController : ControllerBase
    {
        [HttpPost(Name = "Add")]

        public async Task<IActionResult> Add(int usuario, int producto, int precio, int cantidad)
        {
            var carritoToAdd = await new FunLandContext().Carritos.Where(x => x.IdUsuario==usuario).SingleOrDefaultAsync();

            if(carritoToAdd is null)
            {
                carritoToAdd = new Carrito();
                carritoToAdd.Precio = precio;
                carritoToAdd.Cantidad = cantidad;               
            }
            var newCarrito = await new FunLandContext().Carritos.AddAsync(carritoToAdd);

            return Ok(newCarrito);
                    
        }

    }
}
