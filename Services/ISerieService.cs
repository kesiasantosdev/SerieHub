using SerieHubAPI.Dtos;
using SerieHubAPI.Models;

namespace SerieHubAPI.Services
{
    public interface ISerieService
    {
        void Adicionar(int usuarioId, AdicionarSerieDto dto);
        List<Serie> ListarSeriesDoUsuario(int usuarioId);
        Serie? BuscarSerieId(int serieId);
        void Atualizar(Serie AtualizarSerie);
        void Remover(Serie RemoverSerie);
    }
}
