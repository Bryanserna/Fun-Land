using System;
using System.Collections.Generic;

namespace FunLandAPI.Database;

public partial class Review
{
    public int IdReview { get; set; }

    public int? IdUsuario { get; set; }

    public int? IdProducto { get; set; }

    public DateTime? Fecha { get; set; }

    public string? Review1 { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
