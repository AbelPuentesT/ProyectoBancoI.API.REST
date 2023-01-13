namespace ProyectoBanco.Core.Interfaces
{
    public interface IOpcionesPaginacion
    {
        int NumeroPaginaPredeterminado { get; set; }
        int TamanoPaginaPredeterminado { get; set; }
    }
}