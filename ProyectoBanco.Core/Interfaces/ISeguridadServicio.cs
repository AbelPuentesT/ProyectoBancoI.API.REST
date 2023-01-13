using ProyectoBanco.Core.Entidades;

namespace ProyectoBanco.Core.Interfaces
{
    public interface ISeguridadServicio
    {
        Task<Seguridad> OptenerLoginPorCredenciales(UsuarioLogin userlogin);
        Task RegistarCliente(Seguridad seguridad);
    }
}
