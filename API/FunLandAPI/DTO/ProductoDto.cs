using System.ComponentModel.DataAnnotations;

namespace FunLandAPI.DTO
{
    public class ProductoDto
    {
        public int IdProducto { get; set; }
        public int IdClasificacion { get; set; }
        public int IdGenero { get; set; }
        public string NombreProducto { get; set; } = default!;
        public string Descripcion { get; set; } = default!;
        public decimal Precio { get; set; }
        public DateTime FechaLanzamiento { get; set; }
        public bool Activo { get; set; }
    }
}
