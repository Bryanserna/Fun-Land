namespace FunLandAPI.DTO
{
    public record struct GeneroDTO
    {
        public int Id { get; init; }
        public required string Descripcion { get; set; }
        public required bool Activo { get; set; }
    }
}
