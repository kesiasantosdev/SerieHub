using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SerieHubAPI.Data;
using SerieHubAPI.Dtos;
using SerieHubAPI.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace SerieHubAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioHubController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _db;

        public UsuarioHubController(IConfiguration configuration,AppDbContext db)
        {
            _configuration = configuration;
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
                Console.WriteLine($"Ocorreu um erro inesperado: {ex.Message}");
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
                var token = GerarTokenParaUsuario(usuario);
                return Ok(new
                {
                    token,
                    user = new
                    {
                        id = usuario.Id,
                        nome = usuario.Nome,
                        email = usuario.Email
                    }
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Ocorreu um erro no nosso sistema. Tente novamente." });
            }
        }
        private string GerarTokenParaUsuario(Usuarios usuario)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, usuario.Nome),
                new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

