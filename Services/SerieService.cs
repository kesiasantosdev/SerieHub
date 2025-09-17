using SerieHubAPI.Dtos;
using SerieHubAPI.Models;
using SerieHubAPI.Data;

namespace SerieHubAPI.Services 
{
    public class SerieService : ISerieService
    {
        private readonly SerieData _serieData;
    public SerieService(SerieData serieData)
    {
        _serieData = serieData;
    }


        public void Adicionar(int usuarioId, AdicionarSerieDto dto)
        {
            var novaSerie = new Serie
            {
                TmdbId = dto.TmdbId,
                Nome = dto.Nome,
                UrlPoster = dto.UrlPoster,
                TotalTemporadas = dto.TotalTemporada,
                TemporadaAtual = 1,
                UsuarioId = usuarioId,
            };
            _serieData.Adicionar(novaSerie);
        }
    }
}
