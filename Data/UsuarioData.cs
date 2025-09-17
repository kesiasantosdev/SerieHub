using SerieHubAPI.Models;

namespace SerieHubAPI.Data
{
    public class UsuarioData
    {
        private readonly AppDbContext _contexto;

        public UsuarioData(AppDbContext contexto)
        {
            _contexto = contexto;
        }

        public void Adicionar(Usuarios NovoUsuario)
        {
            _contexto.Usuarios.Add(NovoUsuario);
            _contexto.SaveChanges();
        }
        public Usuarios? BuscarPorEmail(string Email)
        {
            return _contexto.Usuarios.FirstOrDefault(u => u.Email == Email);
        }
        public  Usuarios? BuscarPorId(int Id)
        {
            return _contexto.Usuarios.FirstOrDefault(u => u.Id == Id);
        }

    }
}
      
