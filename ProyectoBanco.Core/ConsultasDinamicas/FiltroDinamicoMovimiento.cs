using ProyectoBanco.Core.OpcionesEntidades;

namespace ProyectoBanco.Core.ConsultasDinamicas
{
    public class FiltroDinamicoMovimiento : PaginationFilters
    {
        public string? CliIdentificacion { get; set; } = null!;
        public string? CueNumero { get; set; }

    }
}
