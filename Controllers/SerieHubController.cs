using Microsoft.AspNetCore.Mvc;
using SerieHubAPI.Data;
using SerieHubAPI.Dtos;
using SerieHubAPI.Models;
using SerieHubAPI.Services;

namespace SerieHubAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SerieHubController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly TmdbService _tmdbService;

        public SerieHubController(AppDbContext db, TmdbService tmdbService)
        {
            _db = db;
            _tmdbService = tmdbService;
        }

        [HttpPost]
        public IActionResult AdicionarSerieFavorita([FromBody] AdicionarSerieDto dto)
        {
            try
            {
                var novaSerie = Serie.AdicionarNovaSerieFavorita(dto.UsuarioId, dto.TmdbId, _db, _tmdbService);
                return Ok(novaSerie);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("usuario/{usuarioId}")]
        public IActionResult ListarSeriesDoUsuario(int usuarioId)
        {
            try
            {
                var seriesDoUsuario = Serie.ListarSeriesDoUsuario(usuarioId, _db);

                return Ok(seriesDoUsuario);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpPut("{id}")]
        public IActionResult AtualizarProgressoSerie(int id, [FromBody] AtualizarProgressoDto dto)
        {
            try
            {
                var serie = Serie.BuscarPorId(id, _db);
                if (serie == null)
                {
                    return NotFound(new { message = "Serie não encontrada na sua Lista." });
                }
                serie.AtualizarProgresso(dto.NovaTemporada);
                _db.SaveChanges();
                return Ok(serie);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpDelete("{id}")]
        public IActionResult RemoverSerieFavorita(int id)
        {
            try
            {
                Serie.RemoverSeriePorId(id, _db);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }

}
