using System;
using System.Collections.Generic;

namespace FunLandAPI.Database;

public partial class Genero
{
    public int IdGenero { get; set; }

    public string? Descripcion { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
