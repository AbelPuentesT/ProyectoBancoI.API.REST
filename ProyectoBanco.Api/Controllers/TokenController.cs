using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoBanco.Core.Entidades;
using ProyectoBanco.Infrastructure.Interfaces;

namespace ProyectoBanco.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        public TokenController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        // POST: api/Token
        [HttpPost]
        public async Task<IActionResult> Authentication(UsuarioLogin login)
        {
            var validacion = await _tokenService.ValidarUsuario(login);

            if (validacion.Item1)
            {
                var token = _tokenService.GenerarToken(validacion.Item2);
                return Ok(new { token });
            }
            return NotFound();

        }
    }
}
