namespace SerieHubAPI.Dtos
{
    public class ExibirSerieDto
    {
        public required string Nome { get; set; }
        public required string UrlPoster { get; set; }
        public required int TemporadaAtual { get; set; }
    }
}
