using Microsoft.Extensions.Options;
using ProyectoBanco.Core.Entidades;
using ProyectoBanco.Core.Interfaces;
using ProyectoBanco.Core.OpcionesEntidades;
using ProyectoBanco.Infrastructure.Data;

namespace ProyectoBanco.Infrastructure.Repositorios
{
    public class UnidadDeTrabajo : IUnidadDeTrabajo
    {
        private readonly BancoInterandinoDbContext _bancoInterandinoDbContext;
        private readonly IRepositorioBase<Cliente> _repositorioCliente;
        private readonly IRepositorioMovimiento _repositorioCMovimiento;
        private readonly IRepositorioBase<Cuenta> _repositorioCuenta;
        private readonly IRepositorioSeguridad _repositorioSeguridad;
        private readonly IOpcionesPaginacion _OpcionesPaginacion;
        public UnidadDeTrabajo(BancoInterandinoDbContext bancoInterandinoDbContext, IOptions<OpcionesPaginacion> OpcionesPaginacion)
        {
            _bancoInterandinoDbContext = bancoInterandinoDbContext;
            _OpcionesPaginacion = OpcionesPaginacion.Value;
        }
        public IRepositorioMovimiento RepositorioMovimiento => _repositorioCMovimiento ?? new RepositorioMovimiento(_bancoInterandinoDbContext);
        public IRepositorioBase<Cliente> RepositorioCliente => _repositorioCliente ?? new RepositorioBase<Cliente>(_bancoInterandinoDbContext);
        public IRepositorioBase<Cuenta> RepositorioCuenta => _repositorioCuenta ?? new RepositorioBase<Cuenta>(_bancoInterandinoDbContext);
        public IRepositorioSeguridad RepositorioSeguridad => _repositorioSeguridad ?? new RepositorioSeguridad(_bancoInterandinoDbContext);
        public IOpcionesPaginacion OpcionesPaginacion => _OpcionesPaginacion ?? new OpcionesPaginacion();

        public void Dispose()
        {
            if (_bancoInterandinoDbContext != null)
            {

                _bancoInterandinoDbContext.Dispose();
            }
        }

        public void SaveChanges()
        {
            _bancoInterandinoDbContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _bancoInterandinoDbContext.SaveChangesAsync();
        }
    }
}
