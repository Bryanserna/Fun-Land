using System;
using System.Collections.Generic;

namespace FunLandAPI.Database;

public partial class Carrito
{
    public int? IdUsuario { get; set; }

    public int? IdProducto { get; set; }

    public decimal? Precio { get; set; }

    public int? Cantidad { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
