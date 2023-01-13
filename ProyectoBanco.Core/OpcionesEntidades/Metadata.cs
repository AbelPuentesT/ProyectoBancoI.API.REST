namespace ProyectoBanco.Core.OpcionesEntidades
{
    public class Metadata
    {
        public int PaginaActual { get; set; }
        public int TotalPaginas { get; set; }
        public int TamanoPagina { get; set; }
        public int TotalElementos { get; set; }
        public bool TienePaginaPrevia { get; set; }
        public bool TienePaginaSiguiente { get; set; }
        public string SiguientePaginaURL { get; set; }
        public string AnteriorPaginaURL { get; set; }
        public Metadata(int paginaActual, int totalPaginas, int tamanoPagina, int totalElementos, bool tienePaginaPrevia, bool tienePaginaSiguiente, string siguientePaginaURL, string anteriorPaginaURL)
        {
            PaginaActual = paginaActual;
            TotalPaginas = totalPaginas;
            TamanoPagina = tamanoPagina;
            TotalElementos = totalElementos;
            TienePaginaPrevia = tienePaginaPrevia;
            TienePaginaSiguiente = tienePaginaSiguiente;
            SiguientePaginaURL = siguientePaginaURL;
            AnteriorPaginaURL = anteriorPaginaURL;
        }
        public static Metadata Crear(int paginaActual, int totalPaginas, int tamanoPagina, int totalElementos, bool tienePaginaPrevia, bool tienePaginaSiguiente, string siguientePaginaURL, string anteriorPaginaURL)
        {
            return new Metadata(paginaActual, totalPaginas, tamanoPagina, totalElementos, tienePaginaPrevia, tienePaginaSiguiente, siguientePaginaURL, anteriorPaginaURL);

        }
    }
}
