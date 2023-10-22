using System.ComponentModel.DataAnnotations;

namespace FunLandAPI.DTO
{
    public class ProductoRequestDto
    {
        [Range(1, int.MaxValue)]
        public required int IdGenero { get; set; }

        [Range(1, int.MaxValue)]
        public required int IdClasificacion { get; set; }

        [MinLength(5), MaxLength(50)]
        public required string NombreProducto { get; set; } = default!;

        [MaxLength(200)]
        public required string Descripcion { get; set; } = default!;

        [Range(1, double.MaxValue)]
        public required decimal Precio { get; set; }

        public required DateTime FechaLanzamiento { get; set; }
    }
}
