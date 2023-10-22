namespace FunLandAPI.DTO
{
    public readonly record struct GeneroDTO
    {
        public int Id { get; init; }
        public required string Descripcion { get; init; }
        public required bool Activo { get; init; }
    }
}
