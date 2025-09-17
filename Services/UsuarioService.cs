using SerieHubAPI.Data;
using SerieHubAPI.Dtos;
using SerieHubAPI.Models;

namespace SerieHubAPI.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly UsuarioData _usuarioData;
        public UsuarioService(UsuarioData usuarioData)
        {
            _usuarioData = usuarioData;
        }
        public void RegistrarUsuario(CriarUsuarioDto dto)
        {
            var usuarioExistente = _usuarioData.BuscarPorEmail(dto.Email);
            if ( usuarioExistente != null)
            {
                throw new Exception("Este email já está cadastrado.");
            }
            var senha = BCrypt.Net.BCrypt.HashPassword(dto.Senha);

            var novoUsuario = new Usuarios
            {   
                Nome = dto.Nome,
                Email = dto.Email,
                Senha = senha,
                CriadoEm = DateTime.Now
            };
            _usuarioData.Adicionar(novoUsuario);
        }
        public string Login(LoginUsuarioDto dto)
        {
            return "Login realizado com sucesso!";
        }
    }
}
