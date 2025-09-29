using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SerieHubAPI.Data;
using SerieHubAPI.Dtos;
using SerieHubAPI.Models;
using SerieHubAPI.Services;

namespace SerieHubAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioHubController : ControllerBase
    {
        private readonly AppDbContext _db;

        public UsuarioHubController(AppDbContext db)
        {
            _db = db;
        }
        [HttpPost("registrar")]
        public IActionResult RegistrarUsuario([FromBody] CriarUsuarioDto dto)
        {
            try
            {
                var emailJaExiste = _db.Usuarios.Any(u => u.Email == dto.Email);
                if (emailJaExiste)
                {
                    return BadRequest(new { message = "Este endereço de email já está cadastrado. Tente fazer o login." });
                }

                var novoUsuario = new Usuarios(dto.Nome, dto.Email, dto.Senha);
                novoUsuario.Salvar(_db);
                return Ok(new { message = "Usuário registrado com sucesso!" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro inesperado: {ex.Message}"); // Log para o desenvolvedor ver
                return StatusCode(500, new { message = "Ocorreu um erro no nosso sistema. Por favor, tente novamente mais tarde." });
            }
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginUsuarioDto dto)
        {
            try
            {
                var usuario = Usuarios.BuscarPorEmail(dto.Email, _db);
                if (usuario == null || usuario.Senha != dto.Senha)
                {
                    return Unauthorized(new { message = "Email ou senha inválidos." });
                }
                return Ok(new { message = $"Bem-vindo de volta, {usuario.Nome}!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
