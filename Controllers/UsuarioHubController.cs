using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SerieHubAPI.Dtos;
using SerieHubAPI.Services;

namespace SerieHubAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioHubController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioHubController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }
        [HttpPost("registrar")]
        public IActionResult RegistrarUsuario([FromBody] CriarUsuarioDto dto)
        {
            try
            {
                _usuarioService.RegistrarUsuario(dto);
                return Ok("Usuário registrado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Login")]
        public IActionResult login([FromBody] LoginUsuarioDto dto)
        {
            try
            {
                var token = _usuarioService.Login(dto);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}
