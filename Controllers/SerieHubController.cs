using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SerieHubAPI.Services;

namespace SerieHubAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SerieHubController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public SerieHubController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }
        
    }

}
