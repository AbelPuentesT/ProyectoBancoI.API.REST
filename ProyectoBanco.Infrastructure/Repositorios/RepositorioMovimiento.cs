using Microsoft.EntityFrameworkCore;
using ProyectoBanco.Core.Entidades;
using ProyectoBanco.Core.Interfaces;
using ProyectoBanco.Infrastructure.Data;

namespace ProyectoBanco.Infrastructure.Repositorios
{
    public class RepositorioMovimiento : RepositorioBase<Movimiento>, IRepositorioMovimiento
    {
        public RepositorioMovimiento(BancoInterandinoDbContext bancoInterandinoDbContext) : base(bancoInterandinoDbContext)
        {

        }
        public async Task<IQueryable<Movimiento>> ConsultarMovimientosUsuario(int cliId)
        {
            return (IQueryable<Movimiento>)await _entidades.Where(x => x.CliId == cliId).ToListAsync();
        }
    }
}
