using ProyectoBanco.Core.ConsultasDinamicas;
using ProyectoBanco.Core.Entidades;
using ProyectoBanco.Core.Interfaces;
using ProyectoBanco.Core.OpcionesEntidades;

namespace ProyectoBanco.Core.Servicios
{
    //RepositorioCliente
    public class ClienteServicio : IClienteServicio
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
        public ClienteServicio(IUnidadDeTrabajo unidadDeTrabajo)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        public ListaPaginacion<Cliente> ConsultarTodosLosClientes(FiltroDinamicoCliente filtros)
        {
            filtros.NumeroPagina = filtros.NumeroPagina == 0 ? _unidadDeTrabajo.OpcionesPaginacion.NumeroPaginaPredeterminado : filtros.NumeroPagina;
            filtros.TamanoPagina = filtros.TamanoPagina == 0 ? _unidadDeTrabajo.OpcionesPaginacion.TamanoPaginaPredeterminado : filtros.TamanoPagina;
            var clientes = _unidadDeTrabajo.RepositorioCliente.ConsultarTodos();
            var pagedCliente = ListaPaginacion<Cliente>.Crear(clientes, filtros.NumeroPagina, filtros.TamanoPagina);
            return pagedCliente;
        }
        public async Task<Cliente> ConsultarCliente(int id)
        {
            return await _unidadDeTrabajo.RepositorioCliente.ConsultarPorId(id);
        }

        public async Task CrearCliente(Cliente cliente)
        {
            await _unidadDeTrabajo.RepositorioCliente.Agregar(cliente);
            await _unidadDeTrabajo.SaveChangesAsync();
        }

        public async Task<bool> ModificarCliente(Cliente cliente)
        {
            _unidadDeTrabajo.RepositorioCliente.Actualizar(cliente);
            await _unidadDeTrabajo.SaveChangesAsync();
            return true;
        }
        public async Task<bool> EliminarCliente(int id)
        {
            await _unidadDeTrabajo.RepositorioCliente.Eliminar(id);
            await _unidadDeTrabajo.SaveChangesAsync();
            return true;
        }
    }
}
