using System.Text.Json.Serialization;

namespace SerieHubAPI.Dtos
{
    public class SerieTmdbDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("poster_path")]
        public string PosterPath { get; set; } = string.Empty;

        [JsonPropertyName("number_of_seasons")]
        public int NumberOfSeasons { get; set; }
    }
}
