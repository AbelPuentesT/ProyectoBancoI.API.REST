using ProyectoBanco.Core.Entidades;

namespace ProyectoBanco.Core.Interfaces
{
    public interface IRepositorioBase<T> where T : EntidadBase
    {
        IQueryable<T> ConsultarTodos();
        Task<T> ConsultarPorId(int id);
        Task Agregar(T entity);
        void Actualizar(T entity);
        Task Eliminar(int id);
    }
}