using ProyectoBanco.Core.Entidades;

namespace ProyectoBanco.Core.Interfaces

{
    public interface IRepositorioSeguridad: IRepositorioBase<Seguridad>
    {
        Task<Seguridad> OptenerLoginPorCredenciales(UsuarioLogin login);
    }
}