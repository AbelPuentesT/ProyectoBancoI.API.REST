using ProyectoBanco.Core.Entidades;

namespace ProyectoBanco.Core.Interfaces
{
    public interface IRepositorioMovimiento : IRepositorioBase<Movimiento>
    {
        Task<IQueryable<Movimiento>> ConsultarMovimientosUsuario(int cliId);
    }
}