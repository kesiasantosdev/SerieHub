using System.Text.Json.Serialization;

namespace SerieHubAPI.Dtos
{
    public class TmdbSearchResultDto
    {
        [JsonPropertyName("id")] // Mapeia o "id" do JSON para a propriedade "Id" do C#
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("poster_path")]
        public string Poster_Path { get; set; } = string.Empty;
    }
}
