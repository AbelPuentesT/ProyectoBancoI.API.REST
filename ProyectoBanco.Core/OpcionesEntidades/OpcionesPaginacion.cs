using ProyectoBanco.Core.Interfaces;

namespace ProyectoBanco.Core.OpcionesEntidades

{
    public class OpcionesPaginacion : IOpcionesPaginacion
    {
        public int TamanoPaginaPredeterminado { get; set; }
        public int NumeroPaginaPredeterminado { get; set; }

    }
}
