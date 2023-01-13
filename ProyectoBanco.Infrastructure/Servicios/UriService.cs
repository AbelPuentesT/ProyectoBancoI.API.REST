using ProyectoBanco.Core.ConsultasDinamicas;
using ProyectoBanco.Infrastructure.Interfaces;

namespace ProyectoBanco.Infrastructure.Servicios
{
    public class UriServicio : IUriServicio
    {
        private readonly string _baseUri;
        public UriServicio(string baseUri)
        {
            _baseUri = baseUri;
        }
        public Uri MovimientoPaginacionUri(FiltroDinamicoMovimiento filtros, string accionUrl)
        {
            string baseUrl = $"{_baseUri}{accionUrl}";
            return new Uri(baseUrl);
        }

        //pendiente
        public Uri CuentaPaginacionUri(FiltroDinamicoCuenta filtros, string accionUrl)
        {
            string baseUrl = $"{_baseUri}{accionUrl}";
            return new Uri(baseUrl);
        }
        //pendiente
        public Uri ClientePaginacionUri(FiltroDinamicoCliente filtros, string accionUrl)
        {
            string baseUrl = $"{_baseUri}{accionUrl}";
            return new Uri(baseUrl);
        }
    }
}
