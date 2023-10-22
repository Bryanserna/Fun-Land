namespace FunLandAPI.DTO
{
    public record struct ClasificacionDTO
    {
        public int Id { get; init; }
        public required string Descripcion { get; set; }
        public  required bool Activo { get; set;}
    }
}
