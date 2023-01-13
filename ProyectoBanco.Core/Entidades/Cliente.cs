namespace ProyectoBanco.Core.Entidades;

public partial class Cliente : EntidadBase
{
    public string CliIdentificacion { get; set; } = null!;

    public string CliApellido1 { get; set; } = null!;

    public string CliApellido2 { get; set; } = null!;

    public string CliNombre1 { get; set; } = null!;

    public string CliNombre2 { get; set; } = null!;

    public string CliDireccion { get; set; } = null!;

    public string CliCiudad { get; set; } = null!;

    public string CliCelular { get; set; } = null!;

    public string CliEmail { get; set; } = null!;

    public virtual ICollection<Cuenta> Cuentas { get; } = new List<Cuenta>();

    public virtual ICollection<Movimiento> Movimientos { get; } = new List<Movimiento>();
    public Seguridad Seguridad { get; set; } = null!;

}
