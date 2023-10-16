using System;
using System.Collections.Generic;

namespace FunLandAPI.Database;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string? NombreUsuario { get; set; }

    public string? Correo { get; set; }

    public string? Pass { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<Ventum> Venta { get; set; } = new List<Ventum>();
}
