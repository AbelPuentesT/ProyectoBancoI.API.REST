 namespace ProyectoBanco.Core.Excepciones
{
    public class ExcepcionesDeNegocio : Exception
    {
        public ExcepcionesDeNegocio()
        {

        }
        public ExcepcionesDeNegocio(string message) : base(message)
        {

        }
        public int MyProperty { get; set; }
    }
}
