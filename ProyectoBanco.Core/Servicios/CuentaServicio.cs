using ProyectoBanco.Core.ConsultasDinamicas;
using ProyectoBanco.Core.Entidades;
using ProyectoBanco.Core.Interfaces;
using ProyectoBanco.Core.OpcionesEntidades;

namespace ProyectoBanco.Core.Servicios
{
    public class CuentaServicio : ICuentaServicio
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
        public CuentaServicio(IUnidadDeTrabajo unidadDeTrabajo)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        public ListaPaginacion<Cuenta> ConsultarTodasLasCuentas(FiltroDinamicoCuenta filtros)
        {
            filtros.NumeroPagina = filtros.NumeroPagina == 0 ? _unidadDeTrabajo.OpcionesPaginacion.NumeroPaginaPredeterminado : filtros.NumeroPagina;
            filtros.TamanoPagina = filtros.TamanoPagina == 0 ? _unidadDeTrabajo.OpcionesPaginacion.TamanoPaginaPredeterminado : filtros.TamanoPagina;
            var cuentas = _unidadDeTrabajo.RepositorioCuenta.ConsultarTodos();
            var pagedMovimientos = ListaPaginacion<Cuenta>.Crear(cuentas, filtros.NumeroPagina, filtros.TamanoPagina);
            return pagedMovimientos;
        }
        public async Task<Cuenta> ConsultarCuenta(int id)
        {
            return await _unidadDeTrabajo.RepositorioCuenta.ConsultarPorId(id);
        }

        public async Task CrearCuenta(Cuenta cuenta)
        {
            await _unidadDeTrabajo.RepositorioCuenta.Agregar(cuenta);
            await _unidadDeTrabajo.SaveChangesAsync();
        }

        public async Task<bool> ModificarCuenta(Cuenta cuenta)
        {
            _unidadDeTrabajo.RepositorioCuenta.Actualizar(cuenta);
            await _unidadDeTrabajo.SaveChangesAsync();
            return true;
        }
        public async Task<bool> EliminarCuenta(int id)
        {
            await _unidadDeTrabajo.RepositorioCuenta.Eliminar(id);
            await _unidadDeTrabajo.SaveChangesAsync();
            return true;
        }
    }
}
