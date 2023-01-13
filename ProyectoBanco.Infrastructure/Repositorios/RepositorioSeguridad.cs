using Microsoft.EntityFrameworkCore;
using ProyectoBanco.Core.Entidades;
using ProyectoBanco.Core.Interfaces;
using ProyectoBanco.Infrastructure.Data;

namespace ProyectoBanco.Infrastructure.Repositorios
{
    public class RepositorioSeguridad : RepositorioBase<Seguridad>, IRepositorioSeguridad
    {
        public RepositorioSeguridad(BancoInterandinoDbContext context) : base(context)
        {

        }
        public async Task<Seguridad> OptenerLoginPorCredenciales(UsuarioLogin login)
        {
            return await _entidades.FirstOrDefaultAsync(x => x.SegUsuario == login.Usuario);
        }

        
    }
}