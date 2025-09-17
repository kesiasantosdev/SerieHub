using SerieHubAPI.Dtos;

namespace SerieHubAPI.Services
{
    public interface IUsuarioService
    {
        void RegistrarUsuario(CriarUsuarioDto dto);
        string Login(LoginUsuarioDto dto);

    }
}
