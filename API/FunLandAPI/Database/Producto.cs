using System;
using System.Collections.Generic;

namespace FunLandAPI.Database;

public partial class Producto
{
    public int IdProducto { get; set; }

    public int? IdClasificacion { get; set; }

    public int? IdGenero { get; set; }

    public string? NombreProducto { get; set; }

    public string? Descripcion { get; set; }

    public decimal? Precio { get; set; }

    public DateTime? FechaLanzamiento { get; set; }

    public bool? Activo { get; set; }

    public virtual Clasificacion? IdClasificacionNavigation { get; set; }

    public virtual Genero? IdGeneroNavigation { get; set; }

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<Ventum> Venta { get; set; } = new List<Ventum>();
}
