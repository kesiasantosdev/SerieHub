using SerieHubAPI.Data;
using System.Globalization;
namespace SerieHubAPI.Models
{
    public class Usuarios
    {
        public int Id { get; private set; }
        public string Nome { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string Senha { get; private set; } = string.Empty;
        public DateTime CriadoEm { get; private set; }
        public List<Serie>? Series { get; private set; }

        public Usuarios(string nome, string email, string senha)
        {
            if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha))
            {
                throw new Exception("Nome e email não pode ser vazios");
            }
            Nome = nome;
            Email = email;
            this.Senha = senha;
            CriadoEm = DateTime.Now;
            Series = new List<Serie>();
        }
        public static Usuarios? BuscarPorEmail(string email, AppDbContext db)
        {
            return db.Usuarios.FirstOrDefault(u => u.Email == email);
        }
        public void Salvar(AppDbContext db)
        {
            if (this.Id == 0)
            {
                db.Usuarios.Add(this);
            }
            else 
            {               
                db.Usuarios.Update(this);
            }
            db.SaveChanges();
        }
        public static Usuarios? BuscarPorId(int id, AppDbContext db)
        {
            return db.Usuarios.Find(id);
        }
    } 
}
