using ProyectoBanco.Core.Entidades;

namespace ProyectoBanco.Core.Interfaces
{
    public interface IUnidadDeTrabajo : IDisposable
    {
        IOpcionesPaginacion OpcionesPaginacion { get; }
        IRepositorioBase<Cliente> RepositorioCliente { get; }
        IRepositorioMovimiento RepositorioMovimiento { get; }
        IRepositorioBase<Cuenta> RepositorioCuenta { get; }
        IRepositorioSeguridad RepositorioSeguridad { get; }

        void SaveChanges();
        Task SaveChangesAsync();
    }
}