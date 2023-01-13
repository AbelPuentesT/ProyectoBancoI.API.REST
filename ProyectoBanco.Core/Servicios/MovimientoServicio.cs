using ProyectoBanco.Core.ConsultasDinamicas;
using ProyectoBanco.Core.Entidades;
using ProyectoBanco.Core.Interfaces;
using ProyectoBanco.Core.OpcionesEntidades;

namespace ProyectoBanco.Core.Servicios
{
    public class MovimientoServicio : IMovimientoServicio
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
        public MovimientoServicio(IUnidadDeTrabajo unidadDeTrabajo)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        public ListaPaginacion<Movimiento> ConsultarTodosLosMovimientos(FiltroDinamicoMovimiento filtros)
        {
            filtros.NumeroPagina = filtros.NumeroPagina == 0 ? _unidadDeTrabajo.OpcionesPaginacion.NumeroPaginaPredeterminado : filtros.NumeroPagina;
            filtros.TamanoPagina = filtros.TamanoPagina == 0 ? _unidadDeTrabajo.OpcionesPaginacion.TamanoPaginaPredeterminado : filtros.TamanoPagina;
            var movimientos = _unidadDeTrabajo.RepositorioMovimiento.ConsultarTodos();
            var pagedMovimientos = ListaPaginacion<Movimiento>.Crear(movimientos, filtros.NumeroPagina, filtros.TamanoPagina);
            return pagedMovimientos;
        }
        public async Task<Movimiento> ConsultaMovimiento(int id)
        {
            return await _unidadDeTrabajo.RepositorioMovimiento.ConsultarPorId(id);
        }

        public async Task CrearMovimiento(Movimiento movimiento)
        {
            await _unidadDeTrabajo.RepositorioMovimiento.Agregar(movimiento);
            await _unidadDeTrabajo.SaveChangesAsync();
        }

        public async Task<bool> ModificarMovimiento(Movimiento movimiento)
        {
            _unidadDeTrabajo.RepositorioMovimiento.Actualizar(movimiento);
            await _unidadDeTrabajo.SaveChangesAsync();
            return true;
        }
        public async Task<bool> EliminarMovimiento(int id)
        {
            await _unidadDeTrabajo.RepositorioMovimiento.Eliminar(id);
            await _unidadDeTrabajo.SaveChangesAsync();
            return true;
        }
    }
}
