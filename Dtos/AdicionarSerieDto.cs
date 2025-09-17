namespace SerieHubAPI.Dtos
{
    public class AdicionarSerieDto
    {
        public int TmdbId { get; set; }
        public string Nome { get; set; }
        public string UrlPoster { get; set; }
        public int TotalTemporada { get; set; }
    }
}
