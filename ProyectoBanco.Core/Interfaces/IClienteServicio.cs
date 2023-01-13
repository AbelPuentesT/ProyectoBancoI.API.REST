using ProyectoBanco.Core.ConsultasDinamicas;
using ProyectoBanco.Core.Entidades;
using ProyectoBanco.Core.OpcionesEntidades;

namespace ProyectoBanco.Core.Interfaces
{
    public interface IClienteServicio
    {
        ListaPaginacion<Cliente> ConsultarTodosLosClientes(FiltroDinamicoCliente filtros);
        Task<Cliente> ConsultarCliente(int id);
        Task CrearCliente(Cliente cliente);
        Task<bool> ModificarCliente(Cliente cliente);
        Task<bool> EliminarCliente(int id);
    }
}