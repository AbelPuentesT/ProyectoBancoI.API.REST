using ProyectoBanco.Core.ConsultasDinamicas;
using ProyectoBanco.Core.Entidades;
using ProyectoBanco.Core.OpcionesEntidades;

namespace ProyectoBanco.Core.Interfaces
{
    public interface IMovimientoServicio
    {
        ListaPaginacion<Movimiento> ConsultarTodosLosMovimientos(FiltroDinamicoMovimiento filtros);
        Task<Movimiento> ConsultaMovimiento(int id);
        Task CrearMovimiento(Movimiento movimiento);
        Task<bool> ModificarMovimiento(Movimiento movimiento);
        Task<bool> EliminarMovimiento(int id);
    }
}