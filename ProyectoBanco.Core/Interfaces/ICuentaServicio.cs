using ProyectoBanco.Core.ConsultasDinamicas;
using ProyectoBanco.Core.Entidades;
using ProyectoBanco.Core.OpcionesEntidades;

namespace ProyectoBanco.Core.Interfaces
{
    public interface ICuentaServicio
    {
        ListaPaginacion<Cuenta> ConsultarTodasLasCuentas(FiltroDinamicoCuenta filtros);
        Task<Cuenta> ConsultarCuenta(int id);
        Task CrearCuenta(Cuenta cuenta);
        Task<bool> ModificarCuenta(Cuenta cuenta);
        Task<bool> EliminarCuenta(int id);
    }
}