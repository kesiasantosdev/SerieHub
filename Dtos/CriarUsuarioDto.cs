namespace SerieHubAPI.Dtos
{
    public class CriarUsuarioDto
    {
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required string Senha { get; set; }
    }
}
