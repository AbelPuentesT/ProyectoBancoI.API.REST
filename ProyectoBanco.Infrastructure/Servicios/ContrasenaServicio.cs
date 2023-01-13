using Microsoft.Extensions.Options;
using ProyectoBanco.Core.Excepciones;
using ProyectoBanco.Infrastructure.Interfaces;
using ProyectoBanco.Infrastructure.Opciones;
using System.Security.Cryptography;

namespace ProyectoBanco.Infrastructure.Servicios
{
    public class ContrasenaServicio : IContrasenaServicio
    {
        private readonly ContrasenaOpciones _opciones;
        public ContrasenaServicio(IOptions<ContrasenaOpciones> opciones)
        {
            _opciones = opciones.Value;

        }
        public bool Check(string hash, string password)
        {
            var parts = hash.Split('.');
            if (parts.Length != 3)
            {
                throw new ExcepcionesDeNegocio("Unexpected hash format");
            }
            var iterations = Convert.ToInt32(parts[0]);
            var salt = Convert.FromBase64String(parts[1]);
            var key = Convert.FromBase64String(parts[2]);

            using (var algorithm = new Rfc2898DeriveBytes(
                password,
                salt,
                iterations)
                )
            {
                var keyToCheck = (algorithm.GetBytes(_opciones.KeySize));
                return keyToCheck.SequenceEqual(key);
            }
        }

        public string Hash(string password)
        {
            using (var algorithm = new Rfc2898DeriveBytes(
                password,
                _opciones.SaltSize,
                _opciones.Iterations)
                )
            {
                var key = Convert.ToBase64String(algorithm.GetBytes(_opciones.KeySize));
                var salt = Convert.ToBase64String(algorithm.Salt);
                return $"{_opciones.Iterations}.{salt}.{key}";
            }
        }
    }
}
