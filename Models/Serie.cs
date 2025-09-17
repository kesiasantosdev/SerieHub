namespace SerieHubAPI.Models
{
    public class Serie
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public Usuarios Usuario { get; set; }
        public int TmdbId { get; set; }
        public string Nome { get; set; }
        public string UrlPoster { get; set; }
        public int TemporadaAtual { get; set; }
        public int TotalTemporadas { get; set; }

    }
}
