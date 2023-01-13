using Microsoft.EntityFrameworkCore;
using ProyectoBanco.Core.Entidades;
using ProyectoBanco.Core.Interfaces;
using ProyectoBanco.Infrastructure.Data;

namespace ProyectoBanco.Infrastructure.Repositorios
{
    public class RepositorioBase<T> : IRepositorioBase<T> where T : EntidadBase
    {
        protected readonly BancoInterandinoDbContext _bancoInterandinoDbContext;
        protected readonly DbSet<T> _entidades;
        public RepositorioBase(BancoInterandinoDbContext bancoInterandinoDbContext)
        {
            _bancoInterandinoDbContext = bancoInterandinoDbContext;
            _entidades = _bancoInterandinoDbContext.Set<T>();
        }
        public IQueryable<T> ConsultarTodos()
        {
            return _entidades.AsQueryable();
        }

        public async Task<T> ConsultarPorId(int id)
        {
            return await _entidades.FindAsync(id);
        }
        public async Task Agregar(T entity)
        {
            await _entidades.AddAsync(entity);
        }
        public void Actualizar(T entity)
        {
            _entidades.Update(entity);
        }

        public async Task Eliminar(int id)
        {
            T entityToDelete = await ConsultarPorId(id);
            _entidades.Remove(entityToDelete);
        }
    }
}