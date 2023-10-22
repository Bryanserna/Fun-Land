namespace FunLandAPI.DTO
{
    public readonly record struct ClasificacionDTO
    {
        public int Id { get; init; }
        public required string Descripcion { get; init; }
        public required bool Activo { get; init; }
    }
}
