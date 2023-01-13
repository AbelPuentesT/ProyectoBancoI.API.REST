using ProyectoBanco.Core.ConsultasDinamicas;

namespace ProyectoBanco.Infrastructure.Interfaces
{
    public interface IUriServicio
    {
        Uri MovimientoPaginacionUri(FiltroDinamicoMovimiento filtros, string accionUrl);
        Uri CuentaPaginacionUri(FiltroDinamicoCuenta filtros, string accionUrl);
        Uri ClientePaginacionUri(FiltroDinamicoCliente filtros, string accionUrl);
    }
}