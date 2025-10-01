using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SerieHubAPI.Data;
using SerieHubAPI.Dtos;
using SerieHubAPI.Models;
using SerieHubAPI.Services;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;

namespace SerieHubAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SerieHubController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly TmdbService _tmdbService;

        public SerieHubController(AppDbContext db, TmdbService tmdbService)
        {
            _db = db;
            _tmdbService = tmdbService;
        }

        [HttpPost("adicionar-favorito")]
        [Authorize]

        public async Task<IActionResult> AdicionarSerieFavorita([FromBody] AdicionarSerieDto dto)
        {
            try
            {
                var usuarioIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(usuarioIdString))
                {
                    return Unauthorized();
                }
                var usuarioId = int.Parse(usuarioIdString);

                var novaSerie = await Serie.AdicionarNovaSerieFavoritaAsync(usuarioId, dto.TmdbId, _db, _tmdbService);

                return Ok(novaSerie);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Ocorreu um erro interno ao adicionar serie favorita" });
            }
        }
        [HttpGet("meus-favoritos")]
        [Authorize]
        public IActionResult ListarMinhasSeriesFavoritas()
        {
            try
            {
                var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var seriesDoUsuario = Serie.ListarSeriesDoUsuario(usuarioId, _db);

                var resultadoDto = seriesDoUsuario.Select(serie => new SerieTmdbDto
                {
                    Id = serie.TmdbId,
                    Name = serie.Nome,
                    PosterPath = serie.UrlPoster,
                    NumberOfSeasons = serie.TotalTemporadas
                }).ToList();

                return Ok(resultadoDto);
            }
            catch (Exception )
            {
                return StatusCode(500, new { message = "Ocorreu um erro ao buscar suas series" });
            }
        }


        [HttpGet("serie/{tmdbId}")]
        [AllowAnonymous]
        public async Task<IActionResult> BuscarDetalhesDaSerie(int tmdbId)
        {
            try
            {
                var detalhesDaSerie = await _tmdbService.BuscarSeriePorIdAsync(tmdbId);
                if (detalhesDaSerie == null)
                {
                    return NotFound(new { message = "Série não encontrada na API do TMDB" });
                }
                return Ok(detalhesDaSerie);
            }
            catch (Exception )
            {
                return StatusCode(500, new { message = "Ocorreu um erro ao buscar os detalhes da série" });
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
        [HttpGet("buscar")]
        [AllowAnonymous]
        public async Task<IActionResult> BuscarSeriesPorNome([FromQuery] string nome)
        {
            try
            {
                var resultados = await _tmdbService.BuscarSeriesPorNomeAsync(nome);
                return Ok(resultados);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }

}