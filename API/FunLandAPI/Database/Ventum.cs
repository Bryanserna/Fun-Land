using System;
using System.Collections.Generic;

namespace FunLandAPI.Database;

public partial class Ventum
{
    public int IdVenta { get; set; }

    public int? IdUsuario { get; set; }

    public int? IdProducto { get; set; }

    public int? Cantidad { get; set; }

    public decimal? PrecioU { get; set; }

    public DateTime? Fecha { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
