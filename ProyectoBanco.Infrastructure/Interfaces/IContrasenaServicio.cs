namespace ProyectoBanco.Infrastructure.Interfaces
{
    public interface IContrasenaServicio
    {
        string Hash(string password);
        bool Check(string hash, string password);
    }
}
