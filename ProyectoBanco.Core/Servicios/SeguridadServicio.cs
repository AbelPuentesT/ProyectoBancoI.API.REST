using ProyectoBanco.Core.Entidades;
using ProyectoBanco.Core.Excepciones;
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
            var seguridades = _unidadDeTrabajo.RepositorioSeguridad.ConsultarTodos();
            var segurida = seguridades.FirstOrDefault(x=>x.ClienteID==seguridad.ClienteID);
            if (segurida != null)
            {
                throw new ExcepcionesDeNegocio("El cliente ya posee credenciales de seguridad");
            }
            var usuarios = _unidadDeTrabajo.RepositorioCliente.ConsultarTodos();
            var usuario = usuarios.FirstOrDefault(x => x.Id == seguridad.ClienteID);
            if (usuario == null)
            {
                throw new ExcepcionesDeNegocio("Id de cliente no registrado en la base de datos");
            }
            
            await _unidadDeTrabajo.RepositorioSeguridad.Agregar(seguridad);
            await _unidadDeTrabajo.SaveChangesAsync();
        }
    }
}
