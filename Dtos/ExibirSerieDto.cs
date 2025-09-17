namespace SerieHubAPI.Dtos
{
    public class ExibirSerieDto
    {
        public int Id { get; set; }
        public int TmdbId { get; set; }
        public string Nome { get; set; }
        public string UrlPoster { get; set; }
        public int TemporadaAtual { get; set; }
        public int TotalTemporada { get; set; }
    }
}
