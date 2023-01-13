using ProyectoBanco.Core.Entidades;
using ProyectoBanco.Core.Interfaces;

namespace ProyectoBanco.Core.Servicios
{
    public class SeguridadServicio : ISeguridadServicio
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
        public SeguridadServicio(IUnidadDeTrabajo unidadDeTrabajo)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
        }
        public async Task<Seguridad> OptenerLoginPorCredenciales(UsuarioLogin userlogin)
        {
            return await _unidadDeTrabajo.RepositorioSeguridad.OptenerLoginPorCredenciales(userlogin);
        }
        public async Task RegistarCliente(Seguridad seguridad)
        {
            await _unidadDeTrabajo.RepositorioSeguridad.Agregar(seguridad);
            await _unidadDeTrabajo.SaveChangesAsync(); 

        }
    }
}
