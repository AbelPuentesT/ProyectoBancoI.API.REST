using ProyectoBanco.Core.ConsultasDinamicas;
using ProyectoBanco.Core.Entidades;
using ProyectoBanco.Core.Enumeraciones;
using ProyectoBanco.Core.Excepciones;
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
            if (filtros.CliIdentificacion == null && filtros.CueNumero==null)
            {
                throw new ExcepcionesDeNegocio("Ingrese el numero de identificacion o numero de cuenta que desea consultar");
            }
            var movimientos = _unidadDeTrabajo.RepositorioMovimiento.ConsultarTodos();
            if(filtros.CueNumero!= null)
            {
                var cuentas = _unidadDeTrabajo.RepositorioCuenta.ConsultarTodos();
                var cuenta = cuentas.FirstOrDefault(x => x.CueNumero == filtros.CueNumero);
                if (cuenta == null)
                {
                    throw new ExcepcionesDeNegocio("Numero de cuenta no registrado");
                }
                movimientos = movimientos.Where(x=>x.CueId == cuenta.Id) ;
            }
            if(filtros.CliIdentificacion!= null)
            {
                var clientes = _unidadDeTrabajo.RepositorioCliente.ConsultarTodos();
                var cliente= clientes.FirstOrDefault(x=>x.CliIdentificacion==filtros.CliIdentificacion);
                if (cliente == null)
                {
                    throw new ExcepcionesDeNegocio("Numero de identificacion no registrado");
                }
                movimientos = movimientos.Where(x=>x.CliId==cliente.Id);
            }
            var pagedMovimientos = ListaPaginacion<Movimiento>.Crear(movimientos, filtros.NumeroPagina, filtros.TamanoPagina);
            return pagedMovimientos;
        }
        public async Task<Movimiento> ConsultaMovimiento(int id)
        {
            return await _unidadDeTrabajo.RepositorioMovimiento.ConsultarPorId(id);
        }

        public async Task CrearMovimiento(Movimiento movimiento)
        {
            var cliente = await _unidadDeTrabajo.RepositorioCliente.ConsultarPorId(movimiento.CliId);
            if (cliente == null)
            {
                throw new ExcepcionesDeNegocio("El cliente no se encuentra registrado en la base de datos");
            }

            var cuenta = await _unidadDeTrabajo.RepositorioCuenta.ConsultarPorId(movimiento.CueId);
            if (cuenta == null)
            {
                throw new ExcepcionesDeNegocio("La cuenta no se encuentra registrada en la base de datos");
            }
            if (cuenta.CueActiva == false)
            {
                throw new ExcepcionesDeNegocio("La cuenta no se encuentra activa");
            }
            if (movimiento.MovTipo == TipoMovimiento.Consignacion)
            {
                cuenta.CueSaldoActual += movimiento.MovValor;
                _unidadDeTrabajo.RepositorioCuenta.Actualizar(cuenta);
            }
            if (movimiento.MovTipo == TipoMovimiento.Retiro)
            {
                if (cliente.Id != cuenta.CliId)
                {
                    throw new ExcepcionesDeNegocio("Ingrese la cuenta correcta para este usuario");

                }
                cuenta.CueSaldoActual -= movimiento.MovValor;
                _unidadDeTrabajo.RepositorioCuenta.Actualizar(cuenta);
            }
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
