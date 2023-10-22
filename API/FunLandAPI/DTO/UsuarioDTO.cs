namespace FunLandAPI.DTO
{
    public record struct UsuarioDTO
    {
        public int Id { get; init; }
        public required string Nombre { get; set; }
        public required string Correo { get; set; }

        public required string Password { get; set; }
        public required bool Activo { get; set; }
    }
}
