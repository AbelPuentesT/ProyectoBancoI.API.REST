namespace ProyectoBanco.Core.OpcionesEntidades
{
    public class ListaPaginacion<T>: List<T>
    {
        private readonly OpcionesPaginacion _opcionesPaginacion;
        public int PaginaActual { get; set; }
        public int TotalPaginas { get; set; }
        public int TamanoPagina { get; set; }
        public int TotalElementos { get; set; }
        public bool TienePaginaPrevia => PaginaActual > 1;
        public bool TienePaginaSiguiente => PaginaActual < TotalPaginas;
        public int? NumeroPaginaSiguiente => TienePaginaSiguiente ? PaginaActual+1 : null;
        public int? NumeroPaginaAnterior => TienePaginaPrevia ? PaginaActual - 1 : null;
        public ListaPaginacion(OpcionesPaginacion opciones)
        {
            _opcionesPaginacion = opciones;
        }
        public ListaPaginacion(List<T> items,int totalElementos,int paginaActual, int tamanoPagina)
        {
            TotalElementos= totalElementos;
            PaginaActual= paginaActual;
            TamanoPagina= tamanoPagina;
            TotalPaginas = (int)Math.Ceiling(totalElementos / (double)tamanoPagina);  
            AddRange(items);
        }
        public static ListaPaginacion<T> Crear(IEnumerable<T> elementos,int paginaActual,int tamanoPagina)
        {
            var totalElementos = elementos.Count();
            var items = elementos.Skip((paginaActual-1)*tamanoPagina).Take(tamanoPagina).ToList();
            return new ListaPaginacion<T>(items,totalElementos,paginaActual,tamanoPagina);
        }
        
    }
}
