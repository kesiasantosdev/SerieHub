namespace SerieHubAPI.Models
{
    public class Usuarios
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime CriadoEm { get; set; }

        public List<Serie> Series { get; set; }
    }
}
