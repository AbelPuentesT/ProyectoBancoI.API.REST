using ProyectoBanco.Core.Entidades;

namespace ProyectoBanco.Infrastructure.Interfaces
{
    public interface ITokenService
    {
        Task<(bool, Seguridad)> ValidarUsuario(UsuarioLogin login);
        string GenerarToken(Seguridad seguridad);
    }
}