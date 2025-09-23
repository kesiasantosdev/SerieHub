using SerieHubAPI.Dtos;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace SerieHubAPI.Services
{
    public class TmdbService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public TmdbService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _apiKey = configuration["TmdbSettings:ApiKey"]!;
        }
        public async Task<SerieTmdbDto?> BuscarSeriePorIdAsync(int tmdbId)
        {
            var url = $"https://api.themoviedb.org/3/tv/{tmdbId}?api_key={_apiKey}&language=pt-BR";
            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {                
                return null;
            }
            var content = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var serieDto = JsonSerializer.Deserialize<SerieTmdbDto>(content, options);
            return serieDto;
        }
    }
}
