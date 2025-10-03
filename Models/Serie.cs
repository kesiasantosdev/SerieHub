using SerieHubAPI.Data;
using SerieHubAPI.Services;

namespace SerieHubAPI.Models
{
    public class Serie
    {
        public int Id { get; private set; }
        public int UsuarioId { get; private set; }
        public Usuarios? Usuario { get; private set; }
        public int TmdbId { get; private set; }
        public string Nome { get; private set; } = string.Empty;
        public string UrlPoster { get; private set; } = string.Empty;
        public int TemporadaAtual { get; private set; }
        public int TotalTemporadas { get; private set; }

        public Serie(int usuarioId, int tmdbId, string nome, string urlPoster, int totalTemporadas)
        {
            UsuarioId = usuarioId;
            TmdbId = tmdbId;
            Nome = nome;
            UrlPoster = urlPoster;
            TotalTemporadas = totalTemporadas;
            TemporadaAtual = 1;
        }

        public static Serie? BuscarPorId(int id, AppDbContext db)
        {
            return db.Series.Find(id);
        }


        public static async Task<object> AdicionarNovaSerieFavoritaAsync(int usuarioId, int tmdbId, AppDbContext db, TmdbService tmdbService)
        {
            bool JaExiste = db.Series.Any(s => s.UsuarioId == usuarioId && s.TmdbId == tmdbId);

            if (JaExiste)
            {
                throw new Exception("Série já está na lista de favoritas");
            }

            var dadosDaApi = await tmdbService.BuscarSeriePorIdAsync(tmdbId);

            if (string.IsNullOrEmpty(dadosDaApi?.Name) ||
                string.IsNullOrEmpty(dadosDaApi?.PosterPath) ||
                string.IsNullOrEmpty(dadosDaApi?.NumberOfSeasons.ToString()))
            {
                throw new Exception("Série não encontrada na API do TMDB");
            }

            var novaSerie = new Serie(
                usuarioId,
                tmdbId,
                dadosDaApi.Name,
                $"https://image.tmdb.org/t/p/w500{dadosDaApi.PosterPath}",
                dadosDaApi.NumberOfSeasons
            );

            db.Series.Add(novaSerie);
            await db.SaveChangesAsync();

            return new
            {
                dbid = novaSerie.Id,
                tmdbId,
                name = novaSerie.Nome,
                posterPath = novaSerie.UrlPoster,
                numberOfSeasons = novaSerie.TotalTemporadas,
                temporadaAtual = novaSerie.TemporadaAtual,
            };
        }
        public void AtualizarProgresso(int novaTemporada)
        {
            if (novaTemporada > 0 && novaTemporada <= this.TotalTemporadas)
            {
                this.TemporadaAtual = novaTemporada; 
            }
            else
            {
                
                throw new Exception("Número de temporada inválido");
            }
        }
        public static List<Serie> ListarSeriesDoUsuario(int usuarioId, AppDbContext db)
        {
            return db.Series.Where(s => s.UsuarioId == usuarioId).ToList();
        }

            public static void RemoverSeriePorId(int id, AppDbContext db)
        {
            var serieRemover = db.Series.Find(id);
            if (serieRemover != null)
            {
                db.Series.Remove(serieRemover);
                db.SaveChanges();
            }
            else
            {
                throw new Exception("Série não encontrada na lista deste usuário.");
            }
        }

        
    }
}