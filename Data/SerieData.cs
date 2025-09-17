using SerieHubAPI.Models;

namespace SerieHubAPI.Data
{
    public class SerieData
    {
        private readonly AppDbContext _contexto;

        public SerieData(AppDbContext contexto)
        {
            _contexto = contexto;
        }

       public void Adicionar(Serie novaSerie)
        {
            _contexto.Series.Add(novaSerie);
            _contexto.SaveChanges();
        }

        public List<Serie> ListarSeriesDoUsuario(int usuarioId)
        {            
            return _contexto.Series.Where(s => s.UsuarioId == usuarioId).ToList();
        }

        public Serie? BuscarSerieId(int serieId) => _contexto.Series.Find(serieId);

        public void Atualizar(Serie AtualizarSerie)
        {
            _contexto.Series.Update(AtualizarSerie);
            _contexto.SaveChanges();
        }
        public void Remover(Serie RemoverSerie)
        {
            _contexto.Series.Remove(RemoverSerie);
            _contexto.SaveChanges();
        }




    }

}
