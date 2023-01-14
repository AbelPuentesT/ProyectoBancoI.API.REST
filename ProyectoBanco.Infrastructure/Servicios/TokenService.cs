using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProyectoBanco.Core.Entidades;
using ProyectoBanco.Core.Excepciones;
using ProyectoBanco.Core.Interfaces;
using ProyectoBanco.Infrastructure.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProyectoBanco.Infrastructure.Servicios
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly ISeguridadServicio _securityService;
        private readonly IContrasenaServicio _contrasenaServicio;
        public TokenService(IConfiguration configuration, ISeguridadServicio securityService, IContrasenaServicio contrasenaServicio)
        {
            _configuration = configuration;
            _securityService = securityService;
            _contrasenaServicio = contrasenaServicio;
        }
        public async Task<(bool, Seguridad)> ValidarUsuario(UsuarioLogin login)
        {
            var user = await _securityService.OptenerLoginPorCredenciales(login);
            if (user == null)
            {
                throw new ExcepcionesDeNegocio("Usuario no existe");
            }
            var isValid = _contrasenaServicio.Check(user.SegContrasena, login.Contrasena);
            return (isValid, user);
        }

        public string GenerarToken(Seguridad seguridad)
        {
            //Header 
            var _SymmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"]));
            var signingCredentials = new SigningCredentials(_SymmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingCredentials);
            //Claims
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, seguridad.SegUsuario),
                new Claim("UserName", seguridad.SegNombreUsuario),
                new Claim(ClaimTypes.Role,seguridad.Rol.ToString())
            };
            //Payload
            var payLoad = new JwtPayload
                (
                _configuration["Authentication:Isser"],
                _configuration["Authentication:Audience"],
                claims,
                DateTime.Now,
                DateTime.UtcNow.AddHours(Double.Parse(_configuration["TimeToken:DefaultTimeToken"]))
                ); ;
            var token = new JwtSecurityToken(header, payLoad);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
